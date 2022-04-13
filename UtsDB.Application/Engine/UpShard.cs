using System.IO.MemoryMappedFiles;
using System.IO.Pipelines;
using UtsDB.Domain;
using UtsDB.Domain.Data;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application.Engine;

public class UpShard : IShard
{
    private readonly MemoryMappedFile _shardFile;
    private readonly Stream _viewStream;
    private readonly TableMetadata _tableMetadata;

    public UpShard(MemoryMappedFile shardFile, TableMetadata tableMetadata)
    {
        _shardFile = shardFile;
        _tableMetadata = tableMetadata;
        _viewStream = _shardFile.CreateViewStream(0, _tableMetadata.ShardSize);
    }
    public void Insert(TableMetadata meta, Stream target, ReadOnlySpan<byte> data, DateTime? start = null)
    {
        start ??= meta.EndDate;
        var offset = Helpers.GetDataLength(meta.StartDate, start.Value, meta.Frequency);
        var writeSize = offset * meta.RowWidth;
        target.Seek(writeSize, SeekOrigin.Begin);
        target.Write(data);
        target.Flush();
    }

    public ReadOnlySpan<byte> Read(TableMetadata meta, Stream target, DateTime? start = null, DateTime? end = null)
    {
        start ??= meta.StartDate;
        end ??= meta.EndDate;
        var length = Helpers.GetDataLength(start.Value, end.Value, meta.Frequency);
        var offset = Helpers.GetDataLength(meta.StartDate, start.Value, meta.Frequency);
        var readStart = offset * meta.RowWidth;
        var readSize = length * meta.RowWidth;
        target.Seek(readStart, SeekOrigin.Begin);
        var buffer = new byte[readSize];
        target.Read(buffer, 0, readSize);

        return buffer;
    }

    public async Task Read(Plumber destinationPipe, int start, int end)
    {
        _viewStream.Seek(start, SeekOrigin.Begin);
        var reader = PipeReader.Create(_viewStream);
        var counter = (long)end - start;
        while (counter > 0)
        {
            var result = await reader.ReadAsync();
            var buffer = result.Buffer;
            foreach (var memory in buffer)
            {
                await destinationPipe.SourceWriter.WriteAsync(memory);
            }

            counter -= buffer.Length;
            reader.AdvanceTo(buffer.Start, buffer.End);
        }
        await destinationPipe.SourceWriter.CompleteAsync();
    }

    public async Task Write(Plumber sourcePipe, int start)
    {
        _viewStream.Seek(start, SeekOrigin.Begin);
        while (true)
        {
            var result = await sourcePipe.SourceReader.ReadAsync();
            foreach (var segment in result.Buffer)
            {
                _viewStream.Write(segment.Span);
            }
            sourcePipe.SourceReader.AdvanceTo(result.Buffer.Start, result.Buffer.End);
            if (result.IsCompleted)
                break;
        }

        await sourcePipe.SourceReader.CompleteAsync();
    }

    public Task Destroy()
    {
        throw new NotImplementedException();
    }
}
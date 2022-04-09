using UtsDB.Domain.Data;

namespace UtsDB.Application.Engine;

public class Shard
{
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
}
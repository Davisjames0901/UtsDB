using System.Text.Json;
using UtsDB.Domain;
using UtsDB.Domain.Interfaces;
using UtsDB.Domain.Services;

namespace UtsDB.Application.Engine;

public class TableService : ITableService
{
    private readonly Encoder _encoder;
    private readonly IMetadataService _metadataService;
    private readonly IShardFactory _shardFactory;

    protected TableService(Encoder encoder, IMetadataService metadataService, IShardFactory shardFactory)
    {
        _encoder = encoder;
        _metadataService = metadataService;
        _shardFactory = shardFactory;
    }

    public async Task<List<JsonElement>> Read(string table, DateTime? start = null, DateTime? end = null)
    {
        var tableMetadata = _metadataService.GetTableMetadata(table);
        if(tableMetadata == null)
            throw new Exception("Table does not exist");
        
        start ??= tableMetadata.StartDate;
        end ??= tableMetadata.EndDate;
        var startShard = tableMetadata.Shards.FirstOrDefault(x => x.DateFallsInShard(start.Value));
        if(startShard == null)
            throw new Exception("out of range read");

        return null;
    }

    public Task Write(string table, List<JsonElement> data, DateTime? start = null, DateTime? end = null)
    {
        throw new NotImplementedException();
    }

    public void Delete(string table)
    {
        throw new NotImplementedException();
    }
}

using System.Text.Json;
using UtsDB.Domain.Config;
using UtsDB.Domain.Data;
using UtsDB.Domain.Services;

namespace UtsDB.Application.Services;

public class MetadataService : IMetadataService
{
    private readonly DatabaseConfig _config;
    private readonly Dictionary<string, TableMetadata> _tableLookup;
    
    public MetadataService(DatabaseConfig config)
    {
        _config = config;
        _tableLookup = new();
        var tables = Directory.GetDirectories(_config.TablesDirectory);
        foreach (var table in tables)
        {
            if (File.Exists(_config.TableMetadataPath(table)))
            {
                var json = File.ReadAllText(_config.TableMetadataPath(table));
                var metadata = JsonSerializer.Deserialize<TableMetadata>(json);
                var shards = Directory.GetFiles(_config.ShardMetadataDirectory(table));
                foreach (var shard in shards)
                {
                    var shardJson = File.ReadAllText(shard);
                    var shardMetadata = JsonSerializer.Deserialize<ShardMetadata>(shardJson);
                    metadata.Shards.Add(shardMetadata);
                }
                _tableLookup.Add(metadata.Name, metadata);
            }
        }
    }

    public void CreateTableMetadata(TableMetadata metadata)
    {
        var path = _config.TableMetadataPath(metadata.Name);
        if (!File.Exists(path))
        {
            var json = JsonSerializer.Serialize(metadata);
            File.WriteAllText(path, json);
        }
    }

    public TableMetadata? GetTableMetadata(string table)
    {
        if (_tableLookup.ContainsKey(table))
            return _tableLookup[table];
        return null;
    }

    public void DeleteTableMetadata(string table)
    {
        if (_tableLookup.ContainsKey(table))
            _tableLookup.Remove(table);
        Directory.Delete(_config.TableDirectory(table));
    }

    public ShardMetadata? CreateShardMetadata(string table, GrowthDirection direction)
    {
        if (!_tableLookup.TryGetValue(table, out var tableMetadata))
            return null;
        var shard = new ShardMetadata();
        shard.Direction = direction;
        shard.Id = Guid.NewGuid();
        
        if (direction == GrowthDirection.Up)
        {
            var lastEndDate = tableMetadata.Shards.Max(s => s.End);
            shard.Start = lastEndDate.AdvanceByPeriod(tableMetadata.Frequency);
            shard.End = shard.Start.AdvanceByPeriod(tableMetadata.Frequency, tableMetadata.ShardSize);
        }
        else if (direction == GrowthDirection.Down)
        {
            var firstStartDate = tableMetadata.Shards.Min(s => s.Start);
            shard.Start = firstStartDate.RetardByPeriod(tableMetadata.Frequency);
            shard.End = shard.Start.RetardByPeriod(tableMetadata.Frequency, tableMetadata.ShardSize); 
        }
        tableMetadata.Shards.Add(shard);
        var json = JsonSerializer.Serialize(shard);
        File.WriteAllText(_config.ShardMetadataFile(table, shard.Id), json);
        return shard;
    }

    public void DeleteShardMetadata(string table, Guid id)
    {
        if (_tableLookup.ContainsKey(table))
        {
            _tableLookup[table].Shards.Remove(_tableLookup[table].Shards.Single(s => s.Id == id));
            File.Delete(_config.ShardPath(table, id));
        }
    }
}
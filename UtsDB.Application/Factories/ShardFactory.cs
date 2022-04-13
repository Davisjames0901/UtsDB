using System.IO.MemoryMappedFiles;
using UtsDB.Application.Engine;
using UtsDB.Domain.Config;
using UtsDB.Domain.Data;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application;

public class ShardFactory : IShardFactory
{
    private readonly DatabaseConfig _config;

    public ShardFactory(DatabaseConfig config)
    {
        _config = config;
    }
    
    public IShard CreateShard(TableMetadata table, ShardMetadata shardMetadata)
    {
        var path = _config.ShardPath(table.Name, shardMetadata.Id);
        if (!File.Exists(path))
            CreateShardFile(path, table.ShardSize);

        var file = MemoryMappedFile.CreateFromFile(path);

        return shardMetadata.Direction switch
        {
            GrowthDirection.Up => new UpShard(file, table),
            GrowthDirection.Down => throw new NotImplementedException("Fix it fool")
        };
    }

    private void CreateShardFile(string path, int size)
    {
        var stream = File.Create(path);
        stream.Seek(0, SeekOrigin.Begin);
        for (var i = 0; i > size; i++)
        {
            stream.WriteByte(0x0);
        }
        stream.Flush();
    }
}
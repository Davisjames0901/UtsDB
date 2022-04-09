namespace UtsDB.Domain.Config;

public class DatabaseConfig
{
    public DatabaseConfig(string baseDirectory)
    {
        BaseDir = baseDirectory;
    }
    public string BaseDir { get; set; }
    public string TablesDirectory => $"{BaseDir}{Path.DirectorySeparatorChar}tables";
    public string TableDirectory(string table) => $"{TablesDirectory}{Path.DirectorySeparatorChar}{table}";
    public string ShardPath(string table, Guid shardId) => $"{TableDirectory(table)}{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}shards{Path.DirectorySeparatorChar}{shardId}";
    public string ShardMetadataDirectory(string table) => $"{TableDirectory(table)}{Path.DirectorySeparatorChar}metadata{Path.DirectorySeparatorChar}shards";
    public string ShardMetadataFile(string table, Guid id) => $"{ShardMetadataDirectory(table)}{Path.DirectorySeparatorChar}{id}";
    public string TableMetadataPath(string table) => $"{TableDirectory(table)}{Path.DirectorySeparatorChar}metadata{Path.DirectorySeparatorChar}tablemetadata";

    public void InitPaths()
    {
        if (!Directory.Exists(TablesDirectory))
            Directory.CreateDirectory(TablesDirectory);
    }
}
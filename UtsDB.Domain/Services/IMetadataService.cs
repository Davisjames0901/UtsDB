using UtsDB.Domain.Data;

namespace UtsDB.Domain.Services;

public interface IMetadataService
{
    Task CreateTableMetadata(TableMetadata metadata);
    TableMetadata? GetTableMetadata(string table);
    void DeleteTableMetadata(string table);
    ShardMetadata? CreateShardMetadata(string table, GrowthDirection direction);
    void DeleteShardMetadata(string table, Guid id);
}
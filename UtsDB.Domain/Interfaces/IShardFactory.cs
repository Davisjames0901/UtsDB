using UtsDB.Domain.Data;

namespace UtsDB.Domain.Interfaces;

public interface IShardFactory
{
    IShard CreateShard(TableMetadata table, ShardMetadata metadata);
}
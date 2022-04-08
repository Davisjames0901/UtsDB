using UtsDB.Domain.Data;

namespace UtsDB.Domain.Interfaces;

public interface ITableApi
{
    void Insert(TableMetadata metadata, Stream target, ReadOnlySpan<byte> data, DateTime? startTime = null);
    ReadOnlySpan<byte> Read(TableMetadata metadata, Stream target, DateTime? startDate = null, DateTime? endDate = null);
    
}
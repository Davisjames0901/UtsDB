using System.IO.Pipelines;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application.Strategies;

public class UpsertStrategy : IStrategy
{
    private readonly DateTime _startDate;
    private readonly DateTime _endDate;
    private readonly string _tableName;

    public UpsertStrategy(string tableName, DateTime startDate, DateTime endDate)
    {
        _tableName = tableName;
        _startDate = startDate;
        _endDate = endDate;
    }
    public Task Execute(PipeReader reader, PipeWriter writer)
    {
        throw new NotImplementedException();
    }
}
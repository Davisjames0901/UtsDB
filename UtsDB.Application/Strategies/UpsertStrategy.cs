using System.Text.Json;
using UtsDB.Domain;
using UtsDB.Domain.Interfaces;
using UtsDB.Domain.Options;

namespace UtsDB.Application.Strategies;

public class UpsertStrategy : IStrategy
{
    private readonly WriteOptions _options;

    public UpsertStrategy(WriteOptions options)
    {
        _options = options;
    }

    public Task<List<JsonElement>> Execute(List<JsonElement> data)
    {
        throw new NotImplementedException();
    }
}
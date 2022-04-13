using System.Text.Json;
using UtsDB.Domain;
using UtsDB.Domain.Interfaces;
using UtsDB.Domain.Options;

namespace UtsDB.Application.Strategies;

public class ReadStrategy: IStrategy
{
    private readonly ReadOptions _options;

    public ReadStrategy(ReadOptions options)
    {
        _options = options;
    }

    public Task<List<JsonElement>> Execute(List<JsonElement> data)
    {
        throw new NotImplementedException();
    }
}
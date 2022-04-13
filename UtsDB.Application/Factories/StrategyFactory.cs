using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using UtsDB.Application.Strategies;
using UtsDB.Domain.Data;
using UtsDB.Domain.Interfaces;
using UtsDB.Domain.Options;
using UtsDB.Domain.Services;

namespace UtsDB.Application;

public class StrategyFactory : IStrategyFactory
{
    private readonly IServiceProvider _provider;

    public StrategyFactory(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public IStrategy CreateStrategyFromOptions(JsonElement options)
    {
        var operation = (Operation)options.GetProperty("Operation").GetInt32();
        return operation switch
        {
            Operation.Read => new ReadStrategy(JsonSerializer.Deserialize<ReadOptions>(options)),
            Operation.Insert => new UpsertStrategy(JsonSerializer.Deserialize<WriteOptions>(options)),
            Operation.CreateTable => CreateTableStrategyFromOptions(options),
            _ => throw new NotImplementedException()
        };
    }

    private CreateTableStrategy CreateTableStrategyFromOptions(JsonElement options)
    {
        var createTableOptions = JsonSerializer.Deserialize<CreateTableOptions>(options);
        return new CreateTableStrategy(createTableOptions, _provider.GetService<IMetadataService>());
    }
}
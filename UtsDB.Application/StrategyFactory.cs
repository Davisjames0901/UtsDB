using System.Text.Json;
using UtsDB.Application.Strategies;
using UtsDB.Domain.Data;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application;

public class StrategyFactory : IStrategyFactory
{
    public IStrategy CreateStrategyFromOptions(JsonElement options)
    {
        var operation = (Operation)options.GetProperty("Operation").GetInt32();
        return operation switch
        {
            Operation.Read => new ReadStrategy()
        };
    }
}
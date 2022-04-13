using System.Text.Json;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application.Engine;

public class FrontEnd
{
    private readonly IStrategyFactory _strategyFactory;

    public FrontEnd(IStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }
    
    public Task<List<JsonElement>> Execute(List<JsonElement> data)
    {
        var strategy = _strategyFactory.CreateStrategyFromOptions(data[0]);
        return strategy.Execute(data.Skip(1).ToList());
    }
}
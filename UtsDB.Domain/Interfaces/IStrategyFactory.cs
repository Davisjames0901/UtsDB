using System.Text.Json;

namespace UtsDB.Domain.Interfaces;

public interface IStrategyFactory
{
    IStrategy CreateStrategyFromOptions(JsonElement options);
}
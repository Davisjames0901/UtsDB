using System.IO.Pipelines;
using System.Text.Json;

namespace UtsDB.Domain.Interfaces;

public interface IStrategy
{
    Task<List<JsonElement>> Execute(List<JsonElement> data);
}
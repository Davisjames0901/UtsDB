using System.Text.Json;

namespace UtsDB.Domain.Interfaces;

public interface ITableService
{
    Task<List<JsonElement>> Read(string table, DateTime? start = null, DateTime? end = null);
    Task Write(string table, List<JsonElement> data, DateTime? start = null, DateTime? end = null);
    void Delete(string table);
}
using System.Text.Json;
using UtsDB.Domain.Data;

namespace UtsDB.Domain.Interfaces;

public interface IDb
{
    void CreateTable(TableMetadata meta);
    void DeleteTable(TableMetadata meta);
    TableMetadata GetTableMetaData(string name);
    void Insert(string table, List<JsonElement> data);
    List<JsonElement> Read(string table, DateTime? start = null, DateTime? end = null);
}
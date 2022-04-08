using System.Text.Json;
using UtsDB.Domain.Data;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application.Engine;

public abstract class Db : IDb
{
    private readonly Encoder _encoder;
    private readonly TableApi _tables;
    protected readonly Dictionary<string, (TableMetadata Meta, Stream Target)> TableLookup;
    protected Db(Encoder encoder, TableApi tables)
    {
        _encoder = encoder;
        _tables = tables;
        TableLookup = new();
    }

    public abstract void CreateTable(TableMetadata meta);
    public abstract void DeleteTable(TableMetadata meta);
    protected abstract void UpdateMetadata(TableMetadata meta, DateTime insertStart, DateTime insertEnd);

    public TableMetadata GetTableMetaData(string name) => TableLookup[name].Meta;

    public void Insert(string table, List<JsonElement> data)
    {
        var info = TableLookup[table];
        var encoded = _encoder.EncodeData(info.Meta, data);
        UpdateMetadata(info.Meta, encoded.Start.Value, encoded.End.Value);
        _tables.Insert(info.Meta, info.Target, encoded.Data, encoded.Start);
    }

    public List<JsonElement> Read(string table, DateTime? start = null, DateTime? end = null)
    {
        var info = TableLookup[table];
        var data = _tables.Read(info.Meta, info.Target, start, end);
        return _encoder.DecodeData(info.Meta, data);
    }
}

using System.Text.Json;
using UtsDB.Domain.Config;
using UtsDB.Domain.Data;

namespace UtsDB.Application.Engine;

public class FileDb : Db
{
    private readonly DatabaseConfig _config;
    public FileDb(DatabaseConfig config, Encoder encoder, TableApi tables) : base(encoder, tables)
    {
        _config = config;
        Init();
    }
    
    private void Init()
    {
        if (!Directory.Exists(_config.BaseDir))
            Directory.CreateDirectory(_config.BaseDir);
        if (!Directory.Exists(_config.TableDir))
            Directory.CreateDirectory(_config.TableDir);
        if (!Directory.Exists(_config.ConfigDir))
            Directory.CreateDirectory(_config.ConfigDir);
        if (!File.Exists(_config.TableMetaDataFile))
            WriteTableMetaData();

        var json = File.ReadAllText(_config.TableMetaDataFile);
        var metadata = JsonSerializer.Deserialize<List<TableMetadata>>(json);
        foreach (var table in metadata)
        {
            var stream = new FileStream(_config.TableDir + Path.DirectorySeparatorChar + table.Name, FileMode.OpenOrCreate);
            TableLookup.Add(table.Name, (table, stream));
        }
    }
    
    private void WriteTableMetaData()
    {
        var json = JsonSerializer.Serialize(TableLookup.Values.Select(x=> x.Meta));
        File.WriteAllText(_config.TableMetaDataFile, json);
    }

    public override void CreateTable(TableMetadata meta)
    {
        if (TableLookup.ContainsKey(meta.Name))
            return;
        var stream = new FileStream(_config.TableDir + Path.DirectorySeparatorChar + meta.Name, FileMode.OpenOrCreate);
        TableLookup.Add(meta.Name, (meta, stream));
        WriteTableMetaData();
    }

    public override void DeleteTable(TableMetadata meta)
    {
        throw new NotImplementedException();
    }

    protected override void UpdateMetadata(TableMetadata meta, DateTime insertStart, DateTime insertEnd)
    {
        var changes = false;
        if (meta.StartDate > insertStart)
        {
            meta.StartDate = insertStart;
            changes = true;
        }

        if (meta.EndDate < insertEnd)
        {
            meta.EndDate = insertEnd;
            changes = true;
        }

        if (changes)
        {
            WriteTableMetaData();
        }
    }
}
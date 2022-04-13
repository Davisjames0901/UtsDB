using System.Text.Json.Serialization;

namespace UtsDB.Domain.Data;

public class TableMetadata
{
    public TableMetadata()
    {
        Columns = new ();
        Columns.Add(TableColumn.DateTime("Timestamp"));
        Shards = new();
    }

    public string Name { get; init; }
    public int RowsPerShard { get; init; }
    public DataFrequency Frequency { get; init; }
    public List<TableColumn> Columns { get; init; }
    [JsonIgnore]
    public List<ShardMetadata> Shards { get; set; }
    [JsonIgnore]
    public DateTime? StartDate => Shards?.Min(s=> s.Start);
    [JsonIgnore]
    public DateTime? EndDate => Shards?.Max(s => s.End);
    public int ShardSize => RowsPerShard * RowWidth;
    public int RowWidth => Columns.Sum(x => x.Width);

}
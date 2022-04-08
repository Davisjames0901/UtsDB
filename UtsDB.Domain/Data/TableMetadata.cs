namespace UtsDB.Domain.Data;

public class TableMetadata
{
    public TableMetadata(string name)
    {
        Name = name;
        Columns = new List<TableColumn>();
    }

    public DateTime StartDate { get; set; } = DateTime.MaxValue;
    public DateTime EndDate { get; set; } = DateTime.MinValue;
    public string Name { get; init; }
    public int RowWidth => Columns.Sum(x => x.Width) + 8;
    public DataFrequency Frequency { get; init; }
    public List<TableColumn> Columns { get; init; }
}
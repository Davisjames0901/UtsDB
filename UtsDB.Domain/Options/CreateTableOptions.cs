using UtsDB.Domain.Data;

namespace UtsDB.Domain.Options;

public class CreateTableOptions
{
    public string TableName { get; set; }
    public DataFrequency DataFrequency { get; set; }
    public List<TableColumn> Columns { get; set; }
}
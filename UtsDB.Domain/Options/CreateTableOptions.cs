using UtsDB.Domain.Data;

namespace UtsDB.Domain.Options;

public class CreateTableOptions: Data.Options
{
    public string TableName { get; set; }
    public DataFrequency DataFrequency { get; set; }
    public List<TableColumn> Columns { get; set; }

    public CreateTableOptions() : base(Operation.CreateTable)
    { }
}
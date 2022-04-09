using System.Text.Json;

namespace UtsDB.Domain.Data;

public class TableColumn
{
    public string Name { get; set; }
    public DataTypes Type { get; private init; }
    public int Width { get; private init; }

    public static TableColumn String(string name, int length) => new TableColumn
    {
        Type = DataTypes.String,
        Width = length,
        Name = name
    };

    public static TableColumn Long(string name) => new TableColumn
    {
        Type = DataTypes.Long,
        Width = 8,
        Name = name
    };
    
    public static TableColumn Double(string name) => new TableColumn
    {
        Type = DataTypes.Double,
        Width = 8,
        Name = name
    };
    public static TableColumn DateTime(string name) => new TableColumn
    {
        Type = DataTypes.DateTime,
        Width = 8,
        Name = name
    };
}
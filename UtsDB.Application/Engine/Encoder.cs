using System.Text;
using System.Text.Json;
using UtsDB.Domain.Data;

namespace UtsDB.Application;

public class Encoder
{
    public EncodedData EncodeData(TableMetadata meta, List<JsonElement> data)
    {
        var encodedData = new EncodedData();
        var buffer = new List<byte>();
        foreach (var item in data)
        {
            var date = item.GetProperty("Timestamp").GetDateTime();
            if (encodedData.Start == null || encodedData.Start.Value > date)
                encodedData.Start = date;
            if (encodedData.End == null || encodedData.End.Value < date)
                encodedData.End = date;
            buffer.AddRange(Encoding.Default.GetBytes(date.ToString("MM-dd-yy")));
            foreach (var column in meta.Columns)
            {
                byte[] bytes = column.Type switch
                {
                    DataTypes.Double => BitConverter.GetBytes(item.GetProperty(column.Name).GetDouble()),
                    DataTypes.Long => BitConverter.GetBytes(item.GetProperty(column.Name).GetInt64()),
                    DataTypes.String => Encoding.Default.GetBytes(item.GetProperty(column.Name).GetString() ?? ""),
                    _ => throw new NotImplementedException()
                };
                buffer.AddRange(bytes);
            }
        }

        encodedData.Data = buffer.ToArray();
        return encodedData;
    }

    public List<JsonElement> DecodeData(TableMetadata meta, ReadOnlySpan<byte> data)
    {
        return new();
    }
}
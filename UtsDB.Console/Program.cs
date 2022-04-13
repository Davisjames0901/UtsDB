using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Utsd.Console;
using UtsDB.Application.Engine;
using UtsDB.Domain.Config;
using UtsDB.Domain.Data;
using UtsDB.Domain.Options;

namespace UtsDB.Console;

public class Program
{
    const string tableName = "OMG";
    const int numTestRecords = 0;

    public static async Task Main()
    {
        var config = new DatabaseConfig("testDb");
        var container = ServerBootstrapper.Configure(config);
        var frontend = container.GetService<FrontEnd>();

        //Create random data
        var rand = new Random();
        var data = new List<TestPoint>();
        for (var i = 0; i < numTestRecords; i++)
        {
            data.Add(new()
            {
                Timestamp = DateTime.Today.AddDays(i),
                Value = rand.NextDouble()
            });
        }

        var insertData = data.Select(AsJsonElemnt).ToList();

        //Database operations

        var createTable = new CreateTableOptions
        {
            Columns = new List<TableColumn>
            {
                TableColumn.Double("Value")
            },
            DataFrequency = DataFrequency.Daily,
            TableName = tableName
        };
        var request = AsJsonElemnt(createTable);
        await frontend.Execute(new List<JsonElement>{request});
    }


    private static JsonElement AsJsonElemnt<T>(T data)
    {
        var json = JsonSerializer.Serialize(data);
        return JsonSerializer.Deserialize<JsonElement>(json);
    }
}


//init the db


// //build our test data
// var sww = Stopwatch.StartNew();
// var data = new List<byte>();
// for (var i = 0; i < numTestRecords; i++)
// {
//     var date = Encoding.Default.GetBytes(DateTime.Today.AddDays(i).ToString("MM-dd-yy"));
//     var value = BitConverter.GetBytes(rand.NextDouble());
//     data.AddRange(date);
//     data.AddRange(value);
// }
// //db operations
// engine.InsertIntoTable(tableName, data.ToArray(), DateTime.Today);
// sww.Stop();
// var swr = Stopwatch.StartNew();
// var test = engine.ReadFromTable(tableName, DateTime.Today.AddDays(1), DateTime.Today.AddDays(1000000));
// //deserialize our data
// var readData = new List<(DateTime, double)>();
// var chunk = new byte[16];
// for (var i = 0; i < test.Length; i++)
// {
//     if (i != 0 && i % 16 == 0)
//     {
//         var date = DateTime.Parse(Encoding.Default.GetString(chunk.Take(8).ToArray()));
//         var value = BitConverter.ToDouble(chunk.Skip(8).Take(8).ToArray());
//         readData.Add((date, value));
//     }
//     chunk[i % 16] = test[i];
// }
// swr.Stop();
//
// Console.WriteLine($"Wrote {numTestRecords} records in {sww.ElapsedMilliseconds}ms");
// Console.WriteLine($"Read {readData.Count} records in {swr.ElapsedMilliseconds}ms");
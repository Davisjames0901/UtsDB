using System.Text.Json;
using Utsd.Console;
using UtsDB.Application;
using UtsDB.Application.Engine;
using UtsDB.Domain.Data;
using UtsDB.Domain.Config;

var tableName = "OMG";
var numTestRecords = 1000000;

//init the db
var config = new DatabaseConfig
{
    BaseDir = "testDb"
};
var encoder = new Encoder();
var tableApi = new TableApi();
var engine = new FileDb(config, encoder, tableApi);

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
var json = JsonSerializer.Serialize(data);

//Database operations
var request = JsonSerializer.Deserialize<List<JsonElement>>(json);

engine.CreateTable(new TableMetadata
{
    Name = tableName,
    Frequency = DataFrequency.Daily,
    StartDate = DateTime.Today,
    Columns = new()
    {
        TableColumn.Double("Value")
    }
});

engine.Insert(tableName, request);

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
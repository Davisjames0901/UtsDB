using System.Text.Json;
using UtsDB.Domain.Data;
using UtsDB.Domain.Interfaces;
using UtsDB.Domain.Options;
using UtsDB.Domain.Services;

namespace UtsDB.Application.Strategies;

public class CreateTableStrategy: IStrategy
{
    private readonly CreateTableOptions _options;
    private readonly IMetadataService _metadataService;

    public CreateTableStrategy(CreateTableOptions options, IMetadataService metadataService)
    {
        _options = options;
        _metadataService = metadataService;
    }
    public async Task<List<JsonElement>> Execute(List<JsonElement> data)
    {
        var tableMetadata = new TableMetadata
        {
            Columns = _options.Columns,
            Frequency = _options.DataFrequency,
            Name = _options.TableName
        };
        await _metadataService.CreateTableMetadata(tableMetadata);
        return null;
    }
}
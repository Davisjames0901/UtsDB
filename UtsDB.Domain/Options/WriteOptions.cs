using UtsDB.Domain.Data;

namespace UtsDB.Domain.Options;

public class WriteOptions
{
    public string Table { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
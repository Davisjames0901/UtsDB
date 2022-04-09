using UtsDB.Domain.Data;

namespace UtsDB.Domain.Options;

public class ReadOptions
{
    public string Table { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public DataFrequency Frequency { get; set; }
}
namespace UtsDB.Domain.Data;

public class ShardMetadata
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public GrowthDirection Direction { get; set; }
}
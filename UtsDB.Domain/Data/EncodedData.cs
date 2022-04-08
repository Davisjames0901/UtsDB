namespace UtsDB.Domain.Data;

public class EncodedData
{
    public byte[] Data { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
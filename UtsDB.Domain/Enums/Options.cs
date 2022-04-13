namespace UtsDB.Domain.Data;

public abstract class Options
{
    public Options(Operation operation)
    {
        Operation = operation;
    }

    public Operation Operation { get; }
}
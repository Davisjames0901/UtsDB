using System.IO.Pipelines;

namespace UtsDB.Domain;

public class Plumber
{
    public readonly PipeWriter SourceWriter;
    public readonly PipeReader SourceReader;

    public Plumber(PipeWriter sourceWriter, PipeReader sourceReader)
    {
        SourceWriter = sourceWriter;
        SourceReader = sourceReader;
    }
}
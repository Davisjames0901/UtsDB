namespace UtsDB.Domain.Interfaces;

public interface IShard
{
    Task Read(Plumber destinationPipe, int start, int end);
    Task Write(Plumber sourcePipe, int start);
    Task Destroy();
}
using System.IO.Pipelines;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application.Strategies;

public class ReadStrategy: IStrategy
{
    public ReadStrategy(string table, DateTime start, DateTime end)
    {
        
    }
    public Task Execute(PipeReader reader, PipeWriter writer)
    {
        throw new NotImplementedException();
    }
}
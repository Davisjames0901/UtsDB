using System.IO.Pipelines;
using UtsDB.Domain.Interfaces;

namespace UtsDB.Application.Strategies;

public class CreateTableStrategy: IStrategy
{
    
    public Task Execute(PipeReader reader, PipeWriter writer)
    {
        throw new NotImplementedException();
    }
}
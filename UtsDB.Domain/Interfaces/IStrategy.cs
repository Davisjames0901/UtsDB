using System.IO.Pipelines;

namespace UtsDB.Domain.Interfaces;

public interface IStrategy
{
    Task Execute(PipeReader reader, PipeWriter writer);
}
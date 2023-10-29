namespace Peikcad.Ddd.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task Commit(CancellationToken cancellationToken = default);
}

namespace Peikcad.Ddd.Abstractions;

public interface ICommandRepository<T> : IQueryRepository<T>
    where T : IDomainEntity
{
    IUnitOfWork Uow { get; }

    Task Add(T entity, CancellationToken cancellationToken = default);

    Task Remove(T entity, CancellationToken cancellationToken = default);
}

public interface ICommandRepository<T, in TId> : ICommandRepository<T>, IQueryRepository<T, TId>
    where T : IDomainEntity<TId>
    where TId : notnull
{
    Task<bool> RemoveById(TId id, CancellationToken cancellationToken = default);
}

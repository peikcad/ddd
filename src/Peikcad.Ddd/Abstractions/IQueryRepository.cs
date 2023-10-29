namespace Peikcad.Ddd.Abstractions;

public interface IQueryRepository<T>
    where T : IDomainEntity
{
    Task<T?> GetBySpec(Func<T, bool> spec, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllBySpec(Func<T, bool> spec, CancellationToken cancellationToken = default);
}

public interface IQueryRepository<T, in TId> : IQueryRepository<T>
    where T : IDomainEntity<TId>
    where TId : notnull
{
    Task<T?> GetById(TId id, CancellationToken cancellationToken = default);
}

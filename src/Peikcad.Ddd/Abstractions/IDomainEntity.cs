namespace Peikcad.Ddd.Abstractions;

public interface IDomainEntity
{
    bool IsSameAs(in IDomainEntity comparedEntity);
}

public interface IDomainEntity<out TId> : IDomainEntity
    where TId : notnull
{
    TId Id { get; }
}

public interface IDomainEntity<out TId, out TContext> : IDomainEntity<TId>
    where TId : notnull
    where TContext : IDomainContext<TId>
{
    TContext DomainContext { get; }
}

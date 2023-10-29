#pragma warning disable SA1402

namespace Peikcad.Ddd.DefaultImplementations;

using Peikcad.Ddd.Abstractions;

public abstract class DomainEntity : IDomainEntity
{
    public abstract bool IsSameAs(in IDomainEntity comparedEntity);
}

public abstract class DomainEntity<TId> : DomainEntity, IDomainEntity<TId>
    where TId : notnull
{
    protected DomainEntity(in TId id)
    {
        this.Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public TId Id { get; }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is IDomainEntity comparedEntity &&
               this.IsSameAs(comparedEntity);
    }

    public override string ToString()
    {
        return $"[{this.GetType().Name}::{this.Id}]";
    }

    public override bool IsSameAs(in IDomainEntity comparedEntity)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (comparedEntity is null)
        {
            return false;
        }

        return ReferenceEquals(this, comparedEntity) ||
               (comparedEntity is DomainEntity<TId> entity &&
                EqualityComparer<TId>.Default.Equals(this.Id, entity.Id));
    }
}

public abstract class DomainEntity<TId, TContext> : DomainEntity<TId>, IDomainEntity<TId, TContext>
    where TId : notnull
    where TContext : IDomainContext<TId>
{
    protected DomainEntity(TContext context)
        : base(context.DomainId)
    {
        this.DomainContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public TContext DomainContext { get; }
}

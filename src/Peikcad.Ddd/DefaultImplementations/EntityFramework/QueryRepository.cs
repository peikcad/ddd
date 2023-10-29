#pragma warning disable SA1402

namespace Peikcad.Ddd.DefaultImplementations.EntityFramework;

using Abstractions;
using Microsoft.EntityFrameworkCore;

public class QueryRepository<T> : IQueryRepository<T>
    where T : class, IDomainEntity
{
    public QueryRepository(DbContext context)
    {
        this.Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected DbContext Context { get; }

    public Task<T?> GetBySpec(Func<T, bool> spec, CancellationToken cancellationToken = default)
    {
        if (spec is null)
        {
            throw new ArgumentNullException(nameof(spec));
        }

        return this.GetBySpecInternal(spec, cancellationToken);
    }

    public Task<IEnumerable<T>> GetAllBySpec(Func<T, bool> spec, CancellationToken cancellationToken = default)
    {
        if (spec is null)
        {
            throw new ArgumentNullException(nameof(spec));
        }

        return this.GetAllBySpecInternal(spec, cancellationToken);
    }

    private async Task<T?> GetBySpecInternal(Func<T, bool> spec, CancellationToken cancellationToken)
    {
        return await this.Context.Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(e => spec(e), cancellationToken).ConfigureAwait(false);
    }

    private async Task<IEnumerable<T>> GetAllBySpecInternal(Func<T, bool> spec, CancellationToken cancellationToken)
    {
        return await this.Context.Set<T>()
            .AsNoTracking()
            .Where(e => spec(e))
            .ToArrayAsync(cancellationToken).ConfigureAwait(false);
    }
}

public class QueryRepository<T, TId> : QueryRepository<T>, IQueryRepository<T, TId>
    where T : class, IDomainEntity<TId>
    where TId : notnull
{
    public QueryRepository(DbContext context)
        : base(context)
    {
    }

    public Task<T?> GetById(TId id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        return this.GetByIdInternal(id, cancellationToken);
    }

    private async Task<T?> GetByIdInternal(TId id, CancellationToken cancellationToken)
    {
        return await this.Context.Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken).ConfigureAwait(false);
    }
}

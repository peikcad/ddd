namespace Peikcad.Ddd.Abstractions;

public interface IDomainContext<out TId>
    where TId : notnull
{
    TId DomainId { get; }
}

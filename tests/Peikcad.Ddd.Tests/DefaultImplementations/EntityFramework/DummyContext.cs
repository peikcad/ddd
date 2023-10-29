namespace Peikcad.Ddd.Tests.DefaultImplementations.EntityFramework;

using Peikcad.Ddd.Abstractions;

public class DummyContext : IDomainContext<Guid>
{
    public Guid DomainId => this.Id;

    public Guid Id { get; set; } = Guid.NewGuid();
}

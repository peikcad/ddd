namespace Peikcad.Ddd.Tests.DefaultImplementations.EntityFramework;

using Microsoft.EntityFrameworkCore;

public class SqliteContext : DbContext
{
    public SqliteContext(DbContextOptions<SqliteContext> options)
        : base(options)
    {
    }

    public DbSet<DummyContext> Dummies { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dummyModelBuilder = modelBuilder.Entity<DummyContext>()
            .ToTable("Dummy");

        dummyModelBuilder.HasKey(d => d.Id);

        dummyModelBuilder.Ignore(d => d.DomainId);
    }
}

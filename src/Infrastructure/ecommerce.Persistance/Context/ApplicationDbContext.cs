using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate;
using ecommerce.Domain.Common;
using ecommerce.Persistance.EntityConfigurations;
using ecommerce.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ecommerce.Persistance.Context;
public sealed class ApplicationDbContext : DbContext, IDomainEventProvider, IUnitOfWork {
    private readonly IFactoryInjector factoryInjector;

    public DbSet<CategoryAggregate> Categories => Set<CategoryAggregate>();
    public DbSet<ProductAggregate> Products => Set<ProductAggregate>();
    public DbSet<VariantAggregate> Variants => Set<VariantAggregate>();
    public DbSet<UserAggregate> ApplicationUser => Set<UserAggregate>();

    public ApplicationDbContext(DbContextOptions options, IFactoryInjector factoryInjector) : base(options) {
        this.factoryInjector = factoryInjector;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyEntityConfigurations(factoryInjector);
            //.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        base.OnModelCreating(modelBuilder);
    }

    async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) {
        await SaveChangesAsync(cancellationToken);
    }

    public IEnumerable<IDomainEvent> GetDomainEventsFromEntities() {
        return this.ChangeTracker.Entries<IHasDomainEvent>()
                                 .Where(x => x.Entity.DomainEvents.Any())
                                 .SelectMany(x => x.Entity.DomainEvents);
    }

    public Task CreateExecutionStrategyAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken) {
        IExecutionStrategy strategy = this.Database.CreateExecutionStrategy();
        return strategy.ExecuteAsync(operation, cancellationToken);
    }
}
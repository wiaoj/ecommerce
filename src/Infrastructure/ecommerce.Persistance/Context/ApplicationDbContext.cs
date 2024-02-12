using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate;
using ecommerce.Domain.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ecommerce.Persistance.Context;
public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, Guid>, IDomainEventProvider, IUnitOfWork {
    public DbSet<CategoryAggregate> Categories => Set<CategoryAggregate>();
    public DbSet<ProductAggregate> Products => Set<ProductAggregate>();
    public DbSet<VariantAggregate> Variants => Set<VariantAggregate>();
    public DbSet<UserAggregate> ApplicationUser => Set<UserAggregate>();

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        builder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.Entity<ApplicationUser>()
            .HasOne<UserAggregate>()
            .WithOne()
            .HasForeignKey<ApplicationUser>(user => user.UserId)
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .HasIndex(x => x.UserId)
            .IsUnique();

        base.OnModelCreating(builder);
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
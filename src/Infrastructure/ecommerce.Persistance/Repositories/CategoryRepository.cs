using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using ecommerce.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Persistance.Repositories;
internal sealed class CategoryRepository : Repository<CategoryAggregate, CategoryId, Guid>, ICategoryRepository {
    public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public Task<Boolean> ExistsByNameAsync(CategoryName name, CancellationToken cancellationToken) {
        //TODO StringComparison.InvariantCultureIgnoreCase 
        return this.Query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public Task<List<CategoryAggregate>> FindCategoriesByParentIdAsync(CategoryId id, CancellationToken cancellationToken) {
        return FindCategoriesByParentIdAsync(id, true, cancellationToken);
    }

    public Task<List<CategoryAggregate>> FindCategoriesByParentIdAsync(CategoryId id, Boolean tracking, CancellationToken cancellationToken) {
        IQueryable<CategoryAggregate> query = this.Query.Where(x => x.ParentId == id);
        if(tracking.IsFalse())
            query.AsNoTracking();

        return query.ToListAsync(cancellationToken);
    }
}
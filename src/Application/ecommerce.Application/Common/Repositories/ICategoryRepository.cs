using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;

namespace ecommerce.Application.Common.Repositories;
public interface ICategoryRepository : IRepository<CategoryAggregate, CategoryId, Guid> {
    Task<Boolean> ExistsByNameAsync(CategoryName name, CancellationToken cancellationToken);
    Task<List<CategoryAggregate>> FindCategoriesByParentIdAsync(CategoryId id, CancellationToken cancellationToken);
    Task<List<CategoryAggregate>> FindCategoriesByParentIdAsync(CategoryId id, Boolean tracking, CancellationToken cancellationToken);
}
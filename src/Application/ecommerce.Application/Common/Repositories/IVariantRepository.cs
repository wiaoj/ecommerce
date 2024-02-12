using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;

namespace ecommerce.Application.Common.Repositories;
public interface IVariantRepository : IRepository<VariantAggregate, VariantId, Guid> {
    Task<Boolean> ExistsByNameAsync(String name, CancellationToken cancellationToken);
    Task<IEnumerable<VariantAggregate>> FindByCategoryId(CategoryId categoryId, CancellationToken cancellationToken);
    Task<Boolean> VariantOptionExists(VariantId variantId, VariantOptionValue optionValue, CancellationToken cancellationToken); 
    Task<Boolean> VariantOptionsExistsAsync(IEnumerable<VariantOptionId> ids, CancellationToken cancellationToken);
}
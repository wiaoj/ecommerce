using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using ecommerce.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ecommerce.Persistance.Repositories;
internal sealed class VariantRepository : Repository<VariantAggregate, VariantId, Guid>, IVariantRepository {
    public VariantRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

    public Task<Boolean> ExistsByNameAsync(String name, CancellationToken cancellationToken) {
        return ExistsAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<VariantAggregate>> FindByCategoryId(CategoryId categoryId,
                                                                      CancellationToken cancellationToken) {
        return await this.Query.Where(variant => variant.CategoryId == categoryId).ToListAsync(cancellationToken);
    }

    public Task<Boolean> VariantOptionExists(VariantId variantId,
                                             VariantOptionValue optionValue,
                                             CancellationToken cancellationToken) {
        Expression<Func<VariantAggregate, Boolean>> expression =
            variant => variant.Id == variantId && variant.Options.Any(option => option.Value == optionValue);
        return ExistsAsync(expression, cancellationToken);
    }

    public async Task<Boolean> VariantOptionsExistsAsync(IEnumerable<VariantOptionId> ids,
                                                         CancellationToken cancellationToken) {
        List<VariantOptionId> existingOptionIds = await this.Context.Variants.SelectMany(variant => variant.Options)
                                                                             .Where(option => ids.Contains(option.Id))
                                                                             .Select(variantOption => variantOption.Id)
                                                                             .ToListAsync(cancellationToken);
        return ids.All(existingOptionIds.Contains);
    }
}
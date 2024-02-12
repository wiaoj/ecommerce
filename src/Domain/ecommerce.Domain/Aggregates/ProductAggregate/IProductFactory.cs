using ecommerce.Domain.Aggregates.ProductAggregate.Entities;

namespace ecommerce.Domain.Aggregates.ProductAggregate;
public interface IProductFactory {
    ProductAggregate Create(Guid categoryId, String name, String description);
    ProductVariantEntity CreateVariant(Guid productId,
                                       Int32 stock,
                                       Decimal price,
                                       List<Guid> optionIds);
}
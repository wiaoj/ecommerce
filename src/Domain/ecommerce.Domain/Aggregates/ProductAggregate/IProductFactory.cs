using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.ProductAggregate;
public interface IProductFactory {
    ProductAggregate Create(CategoryId categoryId,
                            ProductName name,
                            ProductDescription description);
    ProductDescription CreateProductDescription(String value);
    ProductId CreateProductId(Guid value);
    ProductName CreateProductName(String value);
    ProductVariantPrice CreateProductVariantPrice(Decimal value);
    ProductVariantEntity CreateVariant(ProductId productId,
                                       Int32 stock,
                                       ProductVariantPrice price,
                                       List<Guid> optionIds);
}
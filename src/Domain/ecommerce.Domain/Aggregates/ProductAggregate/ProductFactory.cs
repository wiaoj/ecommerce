using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.ProductAggregate;
public sealed class ProductFactory : IProductFactory {
    public ProductAggregate Create(CategoryId categoryId, ProductName name, ProductDescription description) {
        ProductAggregate product = new(ProductId.CreateUnique,
                                       categoryId,
                                       name,
                                       description,
                                       []);
        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product));
        return product;
    }

    public ProductVariantEntity CreateVariant(ProductId productId, Int32 stock, ProductVariantPrice price, List<Guid> optionIds) {
        ProductVariantEntity productVariant = new(ProductVariantId.CreateUnique,
                                                  productId,
                                                  String.Empty,
                                                  stock,
                                                  price,
                                                  optionIds.ConvertAll(VariantOptionId.Create));
        return productVariant;
    }

    public ProductId CreateProductId(Guid value) {
        return new(value);
    }

    public ProductName CreateProductName(String value) {
        return new(value);
    }

    public ProductDescription CreateProductDescription(String value) {
        return new(value);
    }

    public ProductVariantPrice CreateProductVariantPrice(Decimal value) {
        return new(value);
    }
}
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects; 

namespace ecommerce.Domain.Aggregates.ProductAggregate;
public sealed class ProductFactory : IProductFactory
{
    public ProductAggregate Create(Guid categoryId, string name, string description)
    {
        ProductAggregate product = new(ProductId.CreateUnique,
                                       CategoryId.Create(categoryId),
                                       ProductName.Create(name),
                                       ProductDescription.Create(description),
                                       []);
        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product));
        return product;
    }

    public ProductVariantEntity CreateVariant(Guid productId, int stock, decimal price, List<Guid> optionIds)
    {
        ProductVariantEntity productVariant = new(ProductVariantId.CreateUnique,
                                                  ProductId.Create(productId),
                                                  string.Empty,
                                                  stock,
                                                  ProductVariantPrice.Create(price),
                                                  optionIds.ConvertAll(VariantOptionId.Create));
        return productVariant;
    }
}
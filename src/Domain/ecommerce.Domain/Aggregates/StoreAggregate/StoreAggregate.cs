using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.StoreAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.StoreAggregate;
public sealed class StoreAggregate : AggregateRoot<StoreId, Guid>
{
    private readonly List<ProductVariantId> productVariantIds;
    public UserId OwnerId { get; private set; }
    public StoreName Name { get; private set; }
    public IReadOnlyCollection<ProductVariantId> ProductVariantIds => productVariantIds.AsReadOnly();

    private StoreAggregate() { }

    internal StoreAggregate(UserId ownerId,
                            StoreId id,
                            StoreName name,
                            List<ProductVariantId> productVariantIds) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
        this.productVariantIds = productVariantIds;
    }

    public StoreAggregate AddProduct(ProductVariantId productVariantId)
    {
        productVariantIds.Add(productVariantId);
        //RaiseDomainEvent(new ProductAddedToVendorDomainEvent(this, productId));
        return this;
    }

    public StoreAggregate RemoveProduct(ProductVariantId productId)
    {
        if (productVariantIds.Contains(productId))
            productVariantIds.Remove(productId);
        return this;
    }

    public override void Delete()
    {
        //RaiseDomainEvent(new VendorDeletedDomainEvent(this));
    }
}
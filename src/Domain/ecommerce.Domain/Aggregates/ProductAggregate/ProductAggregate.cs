using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Common.Models;
using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate;
[DebuggerDisplay("Id: {Id} Name: {Name} Variants: {variants.Count}")]
public sealed class ProductAggregate : AggregateRoot<ProductId, Guid> {
    public CategoryId CategoryId { get; private set; }
    public ProductName Name { get; private set; }
    public ProductDescription Description { get; private set; }

    private readonly List<ProductVariantEntity> variants;
    public IReadOnlyCollection<ProductVariantEntity> Variants => this.variants.AsReadOnly();

    private ProductAggregate() { }
    internal ProductAggregate(ProductId productId,
                              CategoryId categoryId,
                              ProductName productName,
                              ProductDescription productDescription,
                              List<ProductVariantEntity> productVariants) : base(productId) {
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentNullException.ThrowIfNull(categoryId);
        ArgumentNullException.ThrowIfNull(productName);
        ArgumentNullException.ThrowIfNull(productDescription);
        ArgumentNullException.ThrowIfNull(productVariants);
        this.CategoryId = categoryId;
        this.Name = productName;
        this.Description = productDescription;
        this.variants = productVariants;
    }

    public ProductAggregate AddVariant(ProductVariantEntity variant) {
        this.variants.Add(variant);
        return this;
    }

    public ProductAggregate AddVariant(IEnumerable<ProductVariantEntity> variants) {
        this.variants.AddRange(variants);
        return this;
    }

    /// <summary>
    /// <list type="table">
    /// Raises; <br />
    /// <see cref="ProductDeletedDomainEvent"/>
    /// </list>
    /// </summary>
    public override void Delete() {
        RaiseDomainEvent(new ProductDeletedDomainEvent());
    }
}
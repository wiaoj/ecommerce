using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.VariantAggregate.ValueObjects;
using ecommerce.Domain.Common.Models;
using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Entities;
[DebuggerDisplay("Id: {Id} Price: {Price} SKU: {SKU} Stock: {Stock} Options: {optionIds.Count}")]
public sealed class ProductVariantEntity : Entity<ProductVariantId> {
    public ProductId ProductId { get; private set; }
    public ProductVariantPrice Price { get; private set; }
    public String SKU { get; private set; }
    public Int32 Stock { get; private set; }
    public String? Image { get; private set; }
    private readonly List<VariantOptionId> optionIds;
    public IReadOnlyCollection<VariantOptionId> OptionIds => this.optionIds.AsReadOnly();

    private ProductVariantEntity() { }
    internal ProductVariantEntity(ProductVariantId id,
                                  ProductId productId,
                                  String sku,
                                  Int32 stock,
                                  ProductVariantPrice price,
                                  IEnumerable<VariantOptionId> optionIds) : base(id) {
        this.ProductId = productId;
        this.SKU = sku;
        this.Stock = stock;
        this.Price = price;
        this.optionIds = optionIds.ToList();
    }
}
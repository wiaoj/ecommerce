using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
[DebuggerDisplay("{Value}")]
public sealed record ProductVariantPrice {
    public Decimal Value { get; }

    private ProductVariantPrice() { }
    internal ProductVariantPrice(Decimal value) {
        ArgumentNullException.ThrowIfNull(value);
        this.Value = value;
    }

    public static ProductVariantPrice Create(Decimal value) {
        return new(value);
    }

    public static implicit operator Decimal(ProductVariantPrice price) {
        return price.Value;
    }

    public static implicit operator ProductVariantPrice(Decimal price) {
        return new(price);
    }

    public sealed override String ToString() {
        return this.Value.ToString();
    }
}
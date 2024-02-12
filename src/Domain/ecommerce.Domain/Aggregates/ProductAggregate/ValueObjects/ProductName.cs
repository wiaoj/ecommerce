using ecommerce.Domain.Common;
using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects; 
[DebuggerDisplay("{Value}")]
public sealed record ProductName {
    public String Value { get; }

    private ProductName() { }
    private ProductName(String value) {
        this.Value = value;
    }

    public static ProductName Create(String value) {
        value = value.Trim();
        return new(Guard.IfNullOrWhiteSpaceThrow(value));
    }

    public static implicit operator String(ProductName name) {
        return name.Value;
    }
}
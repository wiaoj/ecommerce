using ecommerce.Domain.Common;
using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
[DebuggerDisplay("{Value}")]
public sealed record ProductName {
    public String Value { get; }

    private ProductName() { }
    internal ProductName(String value) {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        value = value.Trim();
        this.Value = value.Trim();
    }

    public static ProductName Create(String value) {
        return new(value);
    }

    public sealed override String ToString() {
        return this.Value;
    }
}
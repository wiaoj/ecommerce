using ecommerce.Domain.Common;
using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
[DebuggerDisplay("{Value}")]
public sealed record ProductDescription {
    public String Value { get; }

    private ProductDescription() { }
    private ProductDescription(String value) {
        this.Value = value;
    }

    public static ProductDescription Create(String value) {
        value = value.Trim();
        return new(Guard.IfNullOrWhiteSpaceThrow(value));
    }
}
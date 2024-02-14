using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
public sealed record CategoryName {
    public String Value { get; }

    private CategoryName() { }
    private CategoryName(String value) {
        this.Value = value;
    }

    public static CategoryName Create(String value) {
        value = value.Trim();
        return new(Guard.IfNullOrWhiteSpaceThrow(value));
    }

    //public static implicit operator CategoryName(String name) => new(name);
    public static implicit operator String(CategoryName name) {
        return name.Value;
    }
}
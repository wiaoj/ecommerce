using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
public sealed record CategoryName {
    public String Value { get; }

    private CategoryName() { }
    internal CategoryName(String value) {
        if(string.IsNullOrWhiteSpace(value))
            throw new InvalidCategoryNameException(value);
        value = value.Trim();
        this.Value = value;
    }

    //public static CategoryName Create(String value) {
    //    value = value.Trim();
    //    return new(Guard.IfNullOrWhiteSpaceThrow(value));
    //}

    //public static implicit operator CategoryName(String name) => new(name);
    public static implicit operator String(CategoryName name) {
        return name.Value;
    }
}
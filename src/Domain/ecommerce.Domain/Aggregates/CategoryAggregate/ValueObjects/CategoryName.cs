using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
public sealed record CategoryName
{
    public string Value { get; }

    private CategoryName() { }
    private CategoryName(string value)
    {
        Value = value;
    }

    public static CategoryName Create(string value)
    {
        value = value.Trim();
        return new(Guard.IfNullOrWhiteSpaceThrow(value));
    }

    //public static implicit operator CategoryName(String name) => new(name);
    public static implicit operator String(CategoryName name) => name.Value;
}
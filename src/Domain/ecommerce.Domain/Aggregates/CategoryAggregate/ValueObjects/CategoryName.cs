using ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
using ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
public sealed record CategoryName {
    public String Value { get; }

    private CategoryName() { }
    internal CategoryName(String value) {
        if(value.IsNullOrWhiteSpaces() || CategoryConstants.Regexes.CategoryNameRegex().IsMatch(value).IsFalse())
            throw new InvalidCategoryNameException(value);

        this.Value = value.Trim();
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
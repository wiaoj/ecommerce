using ecommerce.Domain.Aggregates.ProductAggregate.Constants;
using ecommerce.Domain.Aggregates.ProductAggregate.Exceptions;
using ecommerce.Domain.Extensions;
using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
[DebuggerDisplay("{Value}")]
public sealed record ProductName {
    public String Value { get; }

    private ProductName() { }
    internal ProductName(String value) {
        ValidateForOnlyLettersAndDigits(value);
        ValidateLength(value);
        this.Value = value.Trim();
    }

    public static ProductName Create(String value) {
        return new(value);
    }

    public sealed override String ToString() {
        return this.Value;
    }

    private void ValidateLength(String value) {
        Boolean isLengthInValid = value.Length is < ProductConstants.Rules.Name.MinimumLength
                                               or > ProductConstants.Rules.Name.MaximumLength;
        isLengthInValid.IfTrueThrow<ProductNameLengthOutOfRangeException>();
    }

    private void ValidateForOnlyLettersAndDigits(String value) {
        Boolean condition = value.IsNullOrWhiteSpaces()
            || ProductConstants.Regexes.ProductNameRegex().IsMatch(value).IsFalse();
        condition.IfTrueThrow<ProductNameContainsInvalidCharactersException>();
    }
}
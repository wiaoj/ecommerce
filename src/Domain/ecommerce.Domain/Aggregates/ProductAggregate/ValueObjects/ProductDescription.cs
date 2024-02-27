using ecommerce.Domain.Aggregates.ProductAggregate.Constants;
using ecommerce.Domain.Aggregates.ProductAggregate.Exceptions;
using ecommerce.Domain.Extensions;
using System.Diagnostics;

namespace ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
[DebuggerDisplay("{Value}")]
public sealed record ProductDescription {
    public String Value { get; }

    private ProductDescription() { }
    internal ProductDescription(String value) {
        ValidateForOnlyLettersAndDigits(value);
        ValidateLength(value);
        this.Value = value.Trim();
    }

    public static ProductDescription Create(String value) {
        //ArgumentException.ThrowIfNullOrWhiteSpace(value);
        value = value.Trim();
        return new(value);
    }

    public sealed override String ToString() {
        return this.Value;
    }

    private void ValidateLength(String value) {
        Boolean isLengthInValid = value.Length is < ProductConstants.Rules.Description.MinimumLength
                                               or > ProductConstants.Rules.Description.MaximumLength;
        isLengthInValid.IfTrueThrow<ProductDescriptionLengthOutOfRangeException>();
    }

    private void ValidateForOnlyLettersAndDigits(String value) {
        Boolean condition = value.IsNullOrWhiteSpaces()
            || ProductConstants.Regexes.ProductDescriptionRegex().IsMatch(value).IsFalse();
        condition.IfTrueThrow<ProductDescriptionContainsInvalidCharactersException>();
    }
}
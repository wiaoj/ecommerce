using ecommerce.Domain.Aggregates.ProductAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Exceptions;
internal abstract class ProductNameException(String message) : DomainValidationException(message);

internal sealed class ProductNameLengthOutOfRangeException()
    : ProductNameException(
        ProductConstants.ErrorMessages.ProductNameLengthOutOfRange.Format(
            ProductConstants.Rules.Name.MinimumLength,
            ProductConstants.Rules.Name.MaximumLength));

internal sealed class ProductNameContainsInvalidCharactersException()
    : ProductNameException(ProductConstants.ErrorMessages.ProductNameContainsInvalidCharacters);
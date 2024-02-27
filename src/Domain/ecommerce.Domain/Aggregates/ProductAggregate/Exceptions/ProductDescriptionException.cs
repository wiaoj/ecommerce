using ecommerce.Domain.Aggregates.ProductAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Exceptions;
internal abstract class ProductDescriptionException(String message) : DomainValidationException(message);

internal sealed class ProductDescriptionLengthOutOfRangeException()
    : ProductDescriptionException(
        ProductConstants.ErrorMessages.ProductDescriptionLengthOutOfRange.Format(
            ProductConstants.Rules.Description.MinimumLength,
            ProductConstants.Rules.Description.MaximumLength));

internal sealed class ProductDescriptionContainsInvalidCharactersException()
    : ProductDescriptionException(ProductConstants.ErrorMessages.ProductDescriptionContainsInvalidCharacters);
using ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
public sealed class SubcategoryNotFoundException(CategoryId categoryId)
    : DomainValidationException(CategoryConstants.ErrorMessages.SubcategoryNotFound.Format(categoryId));
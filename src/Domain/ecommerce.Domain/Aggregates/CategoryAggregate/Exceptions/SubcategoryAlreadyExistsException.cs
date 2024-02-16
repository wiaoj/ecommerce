using ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
public sealed class SubcategoryAlreadyExistsException() : DomainValidationException(CategoryConstants.ErrorMessages.SubcategoryAlreadyExists);
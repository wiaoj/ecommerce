using ecommerce.Domain.Aggregates.CategoryAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Exceptions;
internal class InvalidCategoryNameException(String value) 
    : DomainValidationException(CategoryConstants.ErrorMessages.InvalidCategoryName.Format(value));
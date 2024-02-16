using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public sealed class InvalidFirstNameFormatException(String firstName)
    : DomainValidationException(UserConstants.ErrorMessages.InvalidFirstNameCharacters.Format(firstName));
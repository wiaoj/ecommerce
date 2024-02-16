using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public sealed class InvalidEmailFormatException(String? email)
        : DomainValidationException(UserConstants.ErrorMessages.InvalidEmailFormat.Format(email));
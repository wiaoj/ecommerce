using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public sealed class RefreshTokenExpirationException(DateTime created, DateTime expires)
    : DomainValidationException(UserConstants.ErrorMessages.RefreshTokenExpired.Format(created, expires));
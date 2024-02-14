using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public sealed class RefreshTokenExpirationException(DateTime created, DateTime expires)
    : WApplicationException(
        ErrorTypes.Validation,
        "The token's created date '{0}' cannot be after its expiration date '{1}'.".Format(created, expires));
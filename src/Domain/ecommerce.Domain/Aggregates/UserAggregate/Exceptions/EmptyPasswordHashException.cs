using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public class EmptyPasswordHashException()
    : DomainValidationException(UserConstants.ErrorMessages.EmptyPasswordHash);
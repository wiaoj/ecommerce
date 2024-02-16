using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Common.Exceptions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public sealed class PhoneNumberNotRegisteredException()
        : DomainValidationException(UserConstants.ErrorMessages.PhoneNumberNotRegistered);
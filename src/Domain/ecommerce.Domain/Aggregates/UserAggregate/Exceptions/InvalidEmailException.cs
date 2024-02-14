using ecommerce.Domain.Aggregates.UserAggregate.Constans;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public sealed class InvalidEmailException(string email)
        : WApplicationException(ErrorTypes.Validation, UserConstans.Exceptions.InvalidEmailMessage.Format(email));
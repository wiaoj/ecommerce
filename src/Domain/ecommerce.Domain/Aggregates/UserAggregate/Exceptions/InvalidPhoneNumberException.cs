﻿using ecommerce.Domain.Aggregates.UserAggregate.Constans;
using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
public sealed class InvalidPhoneNumberException(string? phoneNumber)
        : DomainValidationException(UserConstans.Exceptions.InvalidPhoneNumberMessage.Format(phoneNumber));
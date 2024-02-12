using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events;
public sealed record UserEmailConfirmedEvent(UserId UserId) : DomainEvent;

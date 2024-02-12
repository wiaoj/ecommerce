using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.UserAggregate.Events;
public sealed record UserDeletedDomainEvent(UserAggregate User) : DomainEvent;
using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Events;
public sealed record CategoryDeletedDomainEvent(CategoryAggregate Category) : DomainEvent;
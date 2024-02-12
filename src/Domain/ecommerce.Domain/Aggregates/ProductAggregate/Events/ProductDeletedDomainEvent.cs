using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events;
public sealed record ProductDeletedDomainEvent() : DomainEvent;
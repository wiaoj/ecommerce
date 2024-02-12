using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.VariantAggregate.Events;
public sealed record VariantDeletedDomainEvent(VariantAggregate VariantAggregate) : DomainEvent;
using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.VariantAggregate.Events;
public sealed record VariantCreatedDomainEvent(VariantAggregate VariantAggregate) : DomainEvent;
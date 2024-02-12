using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.ProductAggregate.Events;
public sealed record ProductVariantCreatedDomainEvent(ProductVariantEntity ProductVariant) : DomainEvent;
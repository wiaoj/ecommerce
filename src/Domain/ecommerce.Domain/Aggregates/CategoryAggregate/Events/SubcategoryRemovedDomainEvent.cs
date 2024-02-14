using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Events;
public sealed record SubcategoryRemovedDomainEvent(CategoryId CategoryId, CategoryId SubcategoryId) : DomainEvent;
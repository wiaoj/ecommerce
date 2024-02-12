using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Events;
public record CategoryCreatedDomainEvent(CategoryAggregate Category) : DomainEvent;
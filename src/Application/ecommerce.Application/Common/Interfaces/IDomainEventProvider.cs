using ecommerce.Domain.Common;

namespace ecommerce.Application.Common.Interfaces;
public interface IDomainEventProvider {
    IEnumerable<IDomainEvent> GetDomainEventsFromEntities();
}
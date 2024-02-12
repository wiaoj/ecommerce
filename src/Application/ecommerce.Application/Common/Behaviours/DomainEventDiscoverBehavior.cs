using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Common;
using ecommerce.Domain.Services;
using MediatR;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class DomainEventDiscoverBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IHasEvent {
    private readonly IDomainEventService domainEventService;
    private readonly IDomainEventProvider domainEventProvider;

    public DomainEventDiscoverBehavior(IDomainEventService domainEventService, IDomainEventProvider domainEventProvider) {
        this.domainEventService = domainEventService;
        this.domainEventProvider = domainEventProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        TResponse response = await next();
        IEnumerable<IDomainEvent> domainEvents = this.domainEventProvider.GetDomainEventsFromEntities();
        this.domainEventService.AddEvents(domainEvents);
        return response;
    }
}
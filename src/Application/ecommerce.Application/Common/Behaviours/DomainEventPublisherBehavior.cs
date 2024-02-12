using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Common;
using ecommerce.Domain.Services;
using MediatR;

namespace ecommerce.Application.Common.Behaviours;
internal sealed class DomainEventPublisherBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IHasEvent {
    private readonly IPublisher publisher;
    private readonly IDomainEventService domainEventService;

    public DomainEventPublisherBehavior(IPublisher publisher, IDomainEventService domainEventService) {
        this.publisher = publisher;
        this.domainEventService = domainEventService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        TResponse response = await next();
        IReadOnlyList<IDomainEvent> domainEvents = this.domainEventService.Events;

        foreach(IDomainEvent domainEvent in domainEvents)
            await this.publisher.Publish(domainEvent, cancellationToken);

        this.domainEventService.ClearEvents();
        return response;
    }
}
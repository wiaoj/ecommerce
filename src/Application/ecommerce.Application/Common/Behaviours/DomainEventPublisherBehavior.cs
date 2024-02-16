using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Common;
using ecommerce.Domain.Extensions;
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

        if(this.domainEventService.Events.Any().IsFalse()) {
            //this.logger.LogInformation("No events found to publish.");
            return response;
        }

        //Int32 eventsCount = this.domainEventService.Events.Count;
        //this.logger.LogInformation("{EventsCount} domain events found to publish for {RequestType}. Initiating publication process...", eventsCount, typeof(TRequest).Name);

        foreach(IDomainEvent domainEvent in this.domainEventService.Events) {
            //this.logger.LogInformation("Publishing domain event {EventId} of type {EventType} associated with {RequestType}.", domainEvent.Id, domainEvent.GetType().Name, typeof(TRequest).Name);
            await this.publisher.Publish(domainEvent, cancellationToken);
            //this.logger.LogInformation("Successfully published domain event {EventId} of type {EventType} associated with {RequestType}.", domainEvent.Id, domainEvent.GetType().Name, typeof(TRequest).Name);
        }

        //this.logger.LogInformation("Successfully published all {EventsCount} domain events for {RequestType}. Proceeding to clear the event queue...", eventsCount, typeof(TRequest).Name);
        //this.logger.LogInformation("Clearing domain events queue for {RequestType}...", typeof(TRequest).Name);
        this.domainEventService.ClearEvents();
        //this.logger.LogInformation("Cleared domain events queue for {RequestType}.", typeof(TRequest).Name);
        return response;
    }
}
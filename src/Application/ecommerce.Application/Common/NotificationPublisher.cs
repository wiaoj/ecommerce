using ecommerce.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ecommerce.Application.Common;
public class DomainEventPublisher : INotificationPublisher {
    private readonly ILogger<DomainEventPublisher> logger;

    public DomainEventPublisher(ILogger<DomainEventPublisher> logger) {
        this.logger = logger;
    }

    public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors,
                              INotification notification,
                              CancellationToken cancellationToken) {

        IDomainEvent? eventInstance = notification as IDomainEvent;

        foreach(NotificationHandlerExecutor handler in handlerExecutors) {
            if(eventInstance != null)
                LogDomainEvent(eventInstance, "Processing");

            await handler.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);

            if(eventInstance != null)
                LogDomainEvent(eventInstance, "Successfully Processed");

        }
    }

    private void LogDomainEvent(IDomainEvent eventInstance, String messagePrefix) {
        this.logger.LogInformation("{messagePrefix} domain event {eventInstanceId} of type {eventInstanceName}",
                                   messagePrefix,
                                   eventInstance.Id,
                                   eventInstance.GetType().Name);
    }
}
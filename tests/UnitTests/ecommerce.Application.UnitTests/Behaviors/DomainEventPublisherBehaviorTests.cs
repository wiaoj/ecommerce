using ecommerce.Application.Common.Behaviours;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Common;
using ecommerce.Domain.Services;
using MediatR;

namespace ecommerce.Application.UnitTests.Behaviors;
public class DomainEventPublisherBehaviorTests {
    private readonly IPublisher publisher;
    private readonly IDomainEventService domainEventService;
    private readonly DomainEventPublisherBehavior<IHasEvent, Object> behavior;
    private readonly IHasEvent request;
    private readonly RequestHandlerDelegate<Object> next;

    public DomainEventPublisherBehaviorTests() {
        this.publisher = Substitute.For<IPublisher>();
        this.domainEventService = Substitute.For<IDomainEventService>();
        this.behavior = new DomainEventPublisherBehavior<IHasEvent, Object>(this.publisher, this.domainEventService);
        this.request = Substitute.For<IHasEvent>();
        this.next = () => Task.FromResult(new Object());
    }

    [Theory]
    [ClassData(typeof(DomainEventPublisherBehaviorTestData))]
    public async Task DomainEventPublisher_ShouldPublishDomainEventsAndClearEvents(Int32 numberOfDomainEvents, Int32 expectedPublishCalls) {
        // Arrange
        List<IDomainEvent> domainEvents = Enumerable.Range(0, numberOfDomainEvents)
                                                    .Select(_ => Substitute.For<IDomainEvent>())
                                                    .ToList();

        this.domainEventService.Events.Returns(domainEvents);

        // Act
        await this.behavior.Handle(this.request, this.next, CancellationToken.None);

        // Assert
        await this.publisher.Received(expectedPublishCalls).Publish(Arg.Any<IDomainEvent>(), Arg.Any<CancellationToken>());
        this.domainEventService.Received(1).ClearEvents();
    }
}
using ecommerce.Application.Common.Behaviours;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Common;
using ecommerce.Domain.Services;
using MediatR;

namespace ecommerce.Application.UnitTests.Behaviors;
public class DomainEventDiscoverBehaviorTests {
    private readonly IDomainEventService domainEventService;
    private readonly IDomainEventProvider applicationDbContext;
    private readonly DomainEventDiscoverBehavior<IHasEvent, Object> behavior;
    private readonly IHasEvent request;
    private readonly RequestHandlerDelegate<Object> next;

    public DomainEventDiscoverBehaviorTests() {
        this.domainEventService = Substitute.For<IDomainEventService>();
        this.applicationDbContext = Substitute.For<IDomainEventProvider>();
        this.behavior = new DomainEventDiscoverBehavior<IHasEvent, Object>(this.domainEventService, this.applicationDbContext);
        this.request = Substitute.For<IHasEvent>();
        this.next = () => Task.FromResult(new Object());
    }

    [Theory]
    [ClassData(typeof(DomainEventDiscoverBehaviorTestData))]
    public async Task DomainEventDiscoverBehavior_ShouldDiscoverAndAddDomainEventsFromEntities(Int32 numberOfEntities) {
        // Arrange
        List<IDomainEvent> domainEvents = Enumerable.Range(0, numberOfEntities)
                                                    .Select(_ => Substitute.For<IDomainEvent>())
                                                    .ToList();

        this.applicationDbContext.GetDomainEventsFromEntities().Returns(domainEvents);

        // Act
        await this.behavior.Handle(this.request, this.next, CancellationToken.None);

        // Assert
        this.applicationDbContext.Received(1).GetDomainEventsFromEntities();
        this.domainEventService.Received(1).AddEvents(Arg.Is<IEnumerable<IDomainEvent>>(events => events.SequenceEqual(domainEvents)));
    }
}
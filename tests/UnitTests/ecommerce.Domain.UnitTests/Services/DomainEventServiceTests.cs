using ecommerce.Domain.Common;
using ecommerce.Domain.Services;
using FluentAssertions;
using NSubstitute;

namespace ecommerce.Domain.UnitTests.Services;
public class DomainEventServiceTests {
    private readonly IDomainEventService domainEventService;
    private readonly IDomainEvent domainEvent;

    public DomainEventServiceTests() {
        this.domainEventService = new DomainEventService();
        this.domainEvent = Substitute.For<IDomainEvent>();
    }

    [Theory]
    [ClassData(typeof(DomainEventServiceTestData))]
    public void AddEvents_ShouldAddSpecifiedNumberOfDomainEvents(Int32 numberOfEvents) {
        // Arrange
        List<IDomainEvent> domainEvents = Enumerable.Range(0, numberOfEvents)
                                     .Select(_ => Substitute.For<IDomainEvent>())
                                     .ToList();

        // Act
        this.domainEventService.AddEvents(domainEvents);

        // Assert
        this.domainEventService.Events.Should().HaveCount(numberOfEvents);
        this.domainEventService.Events.Should().Contain(domainEvents);
    }

    [Fact]
    public void ClearEvents_ShouldRemoveAllDomainEvents() {
        // Arrange
        this.domainEventService.AddEvent(this.domainEvent);

        // Act
        this.domainEventService.ClearEvents();

        // Assert
        this.domainEventService.Events.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(DomainEventServiceTestData))]
    public void AddEvents_ShouldNotAddSameDomainEventMultipleTimes(Int32 repeatCount) {
        // Arrange
        IDomainEvent repeatedDomainEvent = this.domainEvent;
        List<IDomainEvent> repeatedEvents = Enumerable.Repeat(repeatedDomainEvent, repeatCount).ToList();

        // Act
        this.domainEventService.AddEvents(repeatedEvents);

        // Assert
        this.domainEventService.Events.Should().ContainSingle();
        this.domainEventService.Events.Should().Contain(repeatedDomainEvent);
    }

    [Theory]
    [ClassData(typeof(DomainEventServiceTestData))]
    public void ClearEvents_ShouldRemoveSpecifiedDomainEvents(Int32 removeCount) {
        // Arrange
        List<IDomainEvent> eventsToRemove = Enumerable.Range(0, removeCount)
                                       .Select(_ => Substitute.For<IDomainEvent>())
                                       .ToList();
        IDomainEvent remainingEvent = Substitute.For<IDomainEvent>();
        this.domainEventService.AddEvents(eventsToRemove.Concat([remainingEvent]));

        // Act
        this.domainEventService.ClearEvents(eventsToRemove);

        // Assert
        this.domainEventService.Events.Should().ContainSingle();
        this.domainEventService.Events.Should().Contain(remainingEvent);
    }
}
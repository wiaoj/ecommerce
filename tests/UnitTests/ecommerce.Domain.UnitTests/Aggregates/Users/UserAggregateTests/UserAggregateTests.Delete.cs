using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserAggregateTests;
public partial class UserAggregateTests {
    [Fact]
    public void Delete_ShouldRaise_UserDeletedDomainEvent() {
        // Arrange
        UserAggregate user = UserTestFactory.CreateUser();

        // Act
        user.Delete();

        // Assert
        user.DomainEvents.Should().ContainSingle();
        user.DomainEvents[0].Should().BeOfType<UserDeletedDomainEvent>();
        UserDeletedDomainEvent domainEvent = user.DomainEvents.OfType<UserDeletedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.User.Should().Be(user);
    }
}
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserFactoryTests;
public partial class UserFactoryTests {
    [Fact]
    public void Create_ShouldCreateUserAggregate_WithGivenValues() {
        // Arrange
        FullName fullName = UserTestFactory.CreateFullName();
        Email email = UserTestFactory.CreateEmail();
        PhoneNumber phoneNumber = UserTestFactory.CreatePhoneNumber();
        Password password = UserTestFactory.CreatePassword();

        // Act
        UserAggregate user = this.userFactory.Create(fullName, email, phoneNumber, password);

        // Assert
        user.Should().NotBeNull();
        user.FullName.Should().Be(fullName);
        user.Email.Should().Be(email);
        user.PhoneNumber.Should().Be(phoneNumber);
        user.Password.Should().Be(password);
    }

    [Fact]
    public void Create_ShouldRaiseUserCreatedDomainEvent() {
        // Arrange
        FullName fullName = UserTestFactory.CreateFullName();
        Email email = UserTestFactory.CreateEmail();
        PhoneNumber phoneNumber = UserTestFactory.CreatePhoneNumber();
        Password password = UserTestFactory.CreatePassword();

        // Act
        UserAggregate user = this.userFactory.Create(fullName, email, phoneNumber, password);

        // Assert
        user.DomainEvents.Should().ContainSingle();
        user.DomainEvents[0].Should().BeOfType<UserCreatedDomainEvent>();
        UserCreatedDomainEvent domainEvent = user.DomainEvents.OfType<UserCreatedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.User.Should().Be(user);
    }
}
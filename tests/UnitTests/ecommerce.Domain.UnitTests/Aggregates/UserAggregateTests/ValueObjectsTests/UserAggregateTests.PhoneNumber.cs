using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.UserAggregateTests;
public sealed partial class UserAggregateTests {
    [Fact]
    public void CreatePhoneNumber_WithValidValue_ShouldNotBeNullAndNotConfirmed() {
        // Arrange
        String validValue = "123-456-7890";

        // Act
        PhoneNumber phoneNumber = UserTestFactory.CreateExpectedPhoneNumber(validValue);

        // Assert
        phoneNumber.Should().NotBeNull();
        phoneNumber.Value.Should().NotBeNull().And.Be(validValue);
        phoneNumber.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreatePhoneNumber_WithInvalidValue_ShouldBeNullAndNotConfirmed(String? invalidValue) {
        // Act
        PhoneNumber phoneNumber = UserTestFactory.CreateExpectedPhoneNumber(invalidValue);

        // Assert
        phoneNumber.Should().NotBeNull();
        phoneNumber.Value.Should().BeNull();
        phoneNumber.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    public void Confirm_WithInvalidValue_ShouldThrowInvalidPhoneNumberException(String? invalidValue) {
        // Arrange
        PhoneNumber phoneNumber = UserTestFactory.CreateExpectedPhoneNumber(invalidValue);

        // Act
        Func<PhoneNumber> act = () => phoneNumber.Confirm();

        // Assert
        act.Should().Throw<InvalidPhoneNumberException>()
            .WithMessage($"The phone number '{invalidValue}' is invalid.");
    }

    [Fact]
    public void ConfirmPhoneNumber_WithValidValue_ShouldBeConfirmed() {
        // Arrange
        PhoneNumber phoneNumber = UserTestFactory.CreateValidPhoneNumber();

        // Act
        PhoneNumber confirmedPhoneNumber = phoneNumber.Confirm();

        // Assert
        confirmedPhoneNumber.Should().NotBeNull();
        confirmedPhoneNumber.Value.Should().Be(phoneNumber.Value);
        confirmedPhoneNumber.IsConfirmed.Should().BeTrue();
    }
}
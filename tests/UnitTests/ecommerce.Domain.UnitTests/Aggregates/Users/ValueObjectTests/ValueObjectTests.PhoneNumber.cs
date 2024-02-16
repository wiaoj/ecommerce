using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using ecommerce.UnitTests.Common.Users;
using FluentAssertions;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.ValueObjectTests;
public sealed partial class ValueObjectTests {
    [Fact]
    public void CreatePhoneNumber_WithValidValue_ShouldNotBeNullAndNotConfirmed() {
        // Arrange
        String validValue = "123-456-7890";

        // Act
        PhoneNumber phoneNumber = UserTestFactory.CreatePhoneNumber(validValue);

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
        PhoneNumber phoneNumber = UserTestFactory.CreatePhoneNumber(invalidValue);

        // Assert
        phoneNumber.Should().NotBeNull();
        phoneNumber.Value.Should().BeNull();
        phoneNumber.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    public void Confirm_WithInvalidValue_ShouldThrowInvalidPhoneNumberException(String? invalidValue) {
        // Arrange
        PhoneNumber phoneNumber = UserTestFactory.CreatePhoneNumber(invalidValue);

        // Act & Assert
        phoneNumber.Invoking(x => x.Confirm())
            .Should()
            .Throw<PhoneNumberNotRegisteredException>()
            .WithMessage(UserConstants.ErrorMessages.PhoneNumberNotRegistered.Format(invalidValue));
    }

    [Fact]
    public void ConfirmPhoneNumber_WithValidValue_ShouldBeConfirmed() {
        // Arrange
        PhoneNumber phoneNumber = UserTestFactory.CreatePhoneNumber();

        // Act
        PhoneNumber confirmedPhoneNumber = phoneNumber.Confirm();

        // Assert
        confirmedPhoneNumber.Should().NotBeNull();
        confirmedPhoneNumber.Value.Should().Be(phoneNumber.Value);
        confirmedPhoneNumber.IsConfirmed.Should().BeTrue();
    }
}
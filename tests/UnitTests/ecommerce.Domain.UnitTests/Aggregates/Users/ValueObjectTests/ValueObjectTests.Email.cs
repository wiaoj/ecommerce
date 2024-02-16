using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.ValueObjectTests;
public sealed partial class ValueObjectTests {
    [Fact]
    public void CreateEmail_WithValidValue_ShouldNotBeNullAndNotConfirmed() {
        // Arrange
        String validValue = "test@test.com";

        // Act
        Email email = UserTestFactory.CreateEmail(validValue);

        // Assert
        email.Should().NotBeNull();
        email.Value.Should().NotBeNull().And.Be(validValue);
        email.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateEmail_WithInvalidValue_ShouldThrow(String? invalidValue) {
        // Act
        Func<Email> emailFunc = () => UserTestFactory.CreateEmail(invalidValue!);

        // Assert
        emailFunc.Should().Throw<InvalidEmailFormatException>();
    }

    [Fact]
    public void ConfirmEmail_WithValidValue_ShouldBeConfirmed() {
        // Arrange
        Email email = UserTestFactory.CreateEmail();

        // Act
        Email confirmedEmail = email.Confirm();

        // Assert
        confirmedEmail.Should().NotBeNull();
        confirmedEmail.Value.Should().Be(email.Value);
        confirmedEmail.IsConfirmed.Should().BeTrue();
    }
}
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.UserAggregateTests;
public sealed partial class UserAggregateTests {
    [Fact]
    public void CreateEmail_WithValidValue_ShouldNotBeNullAndNotConfirmed() {
        // Arrange
        String validValue = "test@test.com";

        // Act
        Email email = UserTestFactory.CreateExpectedEmail(validValue);

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
        Func<Email> emailFunc = () => UserTestFactory.CreateExpectedEmail(invalidValue!);

        // Assert
        emailFunc.Should().Throw<InvalidEmailException>();
    }

    [Fact]
    public void ConfirmEmail_WithValidValue_ShouldBeConfirmed() {
        // Arrange
        Email email = UserTestFactory.CreateValidEmail();

        // Act
        Email confirmedEmail = email.Confirm();

        // Assert
        confirmedEmail.Should().NotBeNull();
        confirmedEmail.Value.Should().Be(email.Value);
        confirmedEmail.IsConfirmed.Should().BeTrue();
    }
}
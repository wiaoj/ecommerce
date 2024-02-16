using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Extensions;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserFactoryTests;
public partial class UserFactoryTests {
    [Theory]
    [InlineData("test@test.com")]
    public void Create_ValidEmail_ShouldReturnEmail(String email) {
        // Act
        Email createdEmail = this.userFactory.CreateEmail(email);
        // Assert
        createdEmail.Value.Should().Be(email);
        createdEmail.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name+tag@domain.co.uk")]
    public void CreateEmail_ShouldCreateValidEmail_WithValidInput(String validEmail) {
        // Act
        Email email = this.userFactory.CreateEmail(validEmail);

        // Assert
        email.Should().NotBeNull();
        email.Value.Should().Be(validEmail);
        email.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("invalid-email")]
    [InlineData("just@string")]
    [InlineData("@no-local-part.com")]
    [InlineData("no-at-sign")]
    [InlineData("no-domain@.com")]
    public void CreateEmail_ShouldThrowArgumentException_WithInvalidInput(String? invalidEmail) {
        // Act & Assert
        this.userFactory.Invoking(x => x.CreateEmail(invalidEmail!))
            .Should()
            .ThrowExactly<InvalidEmailFormatException>()
            .WithMessage(UserConstants.ErrorMessages.InvalidEmailFormat.Format(invalidEmail));
    }
}
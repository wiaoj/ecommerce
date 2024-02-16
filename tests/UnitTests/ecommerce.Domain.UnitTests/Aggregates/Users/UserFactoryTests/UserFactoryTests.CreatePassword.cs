using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Constants;
using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserFactoryTests;
public partial class UserFactoryTests {
    [Theory]
    [InlineData("password")]
    public void Create_ValidPassword_ShouldReturnPassword(String password) {
        // Arrange
        this.passwordHasher.HashPassword(password, out Arg.Any<String>())
              .Returns(x => {
                  x[1] = "salt";
                  return $"hashed_pass";
              });

        // Act
        Password createdPassword = this.userFactory.CreatePassword(password);

        // Assert
        createdPassword.Should().NotBeNull();
        createdPassword.HashedValue.Should().NotBeNullOrEmpty().And.NotBe(password);
        createdPassword.Salt.Should().NotBeNullOrEmpty().And.NotBe(createdPassword.HashedValue);
    }

    [Fact]
    public void CreatePassword_InvalidPasswordHash_ShouldThrow() {
        // Arrange
        String password = "password";
        this.passwordHasher.HashPassword(password, out Arg.Is<String>(String.Empty))
            .Returns(String.Empty);

        // Act & Assert
        this.userFactory.Invoking(x => x.CreatePassword(password))
            .Should()
            .ThrowExactly<EmptyPasswordHashException>()
            .WithMessage(UserConstants.ErrorMessages.EmptyPasswordHash);
    }

    [Fact]
    public void Create_InvalidPasswordSalt_ShouldThrow() {
        // Arrange
        String password = "password";
        this.passwordHasher.HashPassword(password, out Arg.Any<String>())
            .Returns($"hashed_pass");

        // Act & Assert
        this.userFactory.Invoking(x => x.CreatePassword(password))
            .Should()
            .ThrowExactly<EmptyPasswordSaltException>()
            .WithMessage(UserConstants.ErrorMessages.EmptyPasswordSalt);
    }
}
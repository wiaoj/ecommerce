using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Domain.UnitTests.Aggregates.UserAggregateTests.UserFactoryTests;
public partial class UserFactoryTests {
    [Theory]
    [InlineData(null, "LastName")]
    [InlineData("", "LastName")]
    public void CreateFullName_FirstNameIsNullOrEmpty_ShouldThrow(String firstName, String lastName) {
        // Act
        Func<FullName> func = () => this.userFactory.CreateFullName(firstName, lastName);
        // Assert
        func.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("FirstName", null)]
    [InlineData("FirstName", "")]
    public void CreateFullName_LastNameIsNullOrEmpty_ShouldThrow(String firstName, String lastName) {
        // Act
        Func<FullName> func = () => this.userFactory.CreateFullName(firstName, lastName);
        // Assert
        func.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateEmail_ValueIsNullOrEmpty_ShouldThrow(String email) {
        // Act
        Func<Email> func = () => this.userFactory.CreateEmail(email);
        // Assert
        func.Should().Throw<InvalidEmailException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreatePhoneNumber_ValueIsNullOrEmpty_ShouldNotThrow(String? phoneNumber) {
        // Act
        Func<PhoneNumber> func = () => this.userFactory.CreatePhoneNumber(phoneNumber);
        // Assert
        func.Should().NotThrow<InvalidPhoneNumberException>();
    }

    [Fact]
    public void CreatePassword_InvalidPasswordHash_ShouldThrow() {
        // Arrange
        String password = "password";
        this.passwordHasher.HashPassword(password, out Arg.Any<String>())
            .Returns($"hashed_pass");

        // Act
        Func<Password> createdPasswordFunc = () => this.userFactory.CreatePassword(password);

        // Assert
        createdPasswordFunc.Should().Throw<ArgumentException>("Password Hash or Salt can not be null");
    }

    [Fact]
    public void CreateRefreshToken_CreatedBiggerThanExpires_ShouldThrow() {
        // Arrange
        String token = "token";
        DateTime created = DateTime.UtcNow.AddMinutes(1);
        DateTime expires = DateTime.UtcNow;
        String ipAddress = "::1";

        // Act
        Func<RefreshToken> func = () => this.userFactory.CreateRefreshToken(token, created, expires, ipAddress);

        // Assert
        func.Should().Throw<RefreshTokenExpirationException>();
    }
}
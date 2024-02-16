using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.UserAggregateTests;
public partial class UserFactoryTests {
    private readonly IPasswordHasher passwordHasher;
    private readonly IUserFactory userFactory;
    public UserFactoryTests() {
        this.passwordHasher = Substitute.For<IPasswordHasher>();
        this.userFactory = new UserFactory(this.passwordHasher);
    }

    [Theory]
    [InlineData("John", "Doe")]
    public void Create_ValidFullName_ShouldReturnFullName(String firstName,
                                                          String lastName) {
        // Act
        FullName createdFullName = this.userFactory.CreateFullName(firstName, lastName);
        // Assert
        createdFullName.FirstName.Should().Be(firstName);
        createdFullName.LastName.Should().Be(lastName);
    }

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
    [InlineData(null)]
    [InlineData("5551112233")]
    public void Create_ValidPhoneNumber_ShouldReturnPhoneNumber(String? phoneNumber) {
        // Act
        PhoneNumber createdPhoneNumber = this.userFactory.CreatePhoneNumber(phoneNumber);
        // Assert
        createdPhoneNumber.Value.Should().Be(phoneNumber);
        createdPhoneNumber.IsConfirmed.Should().BeFalse();
    }

    [Theory]
    [InlineData("token", "Ip")]
    public void Create_ValidRefreshToken_ShouldReturnRefreshToken(String token,
                                                                  String createdByIp) {
        // Arrange
        DateTime created = DateTime.UtcNow;
        DateTime expires = DateTime.UtcNow.AddMinutes(1);

        // Act
        RefreshToken createdRefreshToken = this.userFactory.CreateRefreshToken(token,
                                                                               created,
                                                                               expires,
                                                                               createdByIp);
        // Assert
        createdRefreshToken.Token.Should().Be(token);
        createdRefreshToken.Created.Should().Be(created);
        createdRefreshToken.Expires.Should().Be(expires);
        createdRefreshToken.CreatedByIp.Should().Be(createdByIp);
    }

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
    public void Create_InvalidPasswordSalt_ShouldThrow() {
        // Arrange
        String password = "password";
        this.passwordHasher.HashPassword(password, out Arg.Is<String>(String.Empty))
            .Returns(String.Empty);

        // Act
        Func<Password> createdPasswordFunc = () => this.userFactory.CreatePassword(password);

        // Assert
        createdPasswordFunc.Should().Throw<ArgumentException>("Password Hash or Salt can not be null");
    }

    [Fact]
    public void Create_ReturnsValidUserAggregate() {
        // Arrange
        FullName fullName = UserTestFactory.CreateValidFullName();
        Email email = UserTestFactory.CreateValidEmail();
        PhoneNumber phoneNumber = UserTestFactory.CreateValidPhoneNumber();
        Password password = UserTestFactory.CreateValidPassword();

        // Act
        UserAggregate user = this.userFactory.Create(fullName, email, phoneNumber, password);

        // Assert
        user.Should().NotBeNull();
        user.FullName.Should().Be(fullName);
        user.Email.Should().Be(email);
        user.PhoneNumber.Should().Be(phoneNumber);
        user.Password.Should().Be(password);
        user.DomainEvents.Should().ContainSingle();
        user.DomainEvents[0].Should().BeOfType<UserCreatedDomainEvent>();
        UserCreatedDomainEvent domainEvent = user.DomainEvents.OfType<UserCreatedDomainEvent>().First();
        domainEvent.Should().NotBeNull();
        domainEvent.User.Should().Be(user);
    }
}
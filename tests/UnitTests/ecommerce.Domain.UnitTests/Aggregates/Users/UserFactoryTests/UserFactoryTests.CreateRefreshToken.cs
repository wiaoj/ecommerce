using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserFactoryTests;
public partial class UserFactoryTests {
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
        createdRefreshToken.IsExpired.Should().BeFalse();
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
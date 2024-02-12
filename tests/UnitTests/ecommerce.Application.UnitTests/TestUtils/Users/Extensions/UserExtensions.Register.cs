using ecommerce.Application.Features.Authentication.Commands.Register;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using FluentAssertions;

namespace ecommerce.Application.UnitTests.TestUtils.Users.Extensions;
public static partial class UserExtensions {
    public static void VerifyUserCreationFromCommand(this UserAggregate user, RegisterCommand command) {
        user.FullName.FirstName.Should().Be(command.FirstName);
        user.FullName.MiddleNames.Should().BeEmpty(); //For now, we don't have middle names
        user.FullName.LastName.Should().Be(command.LastName);
        user.Email.Value.Should().Be(command.Email);
        user.PhoneNumber.Value.Should().Be(command.PhoneNumber);
        user.Password.HashedValue.Should().NotBeEmpty();
        user.Password.Salt.Should().NotBeEmpty();
    }

    public static void ValidateRefreshToken(this UserAggregate user, RefreshToken expectedRefreshToken) {
        user.RefreshTokens.Should().ContainSingle();
        RefreshToken refreshToken = user.RefreshTokens.Single();
        refreshToken.Token.Should().NotBeEmpty();
        refreshToken.Token.Should().Be(expectedRefreshToken.Token);
        refreshToken.Expires.Should().BeCloseTo(expectedRefreshToken.Expires, precision: TimeSpan.FromSeconds(1));
        refreshToken.Created.Should().BeCloseTo(expectedRefreshToken.Created, precision: TimeSpan.FromSeconds(1));
        refreshToken.CreatedByIp.Should().Be(expectedRefreshToken.CreatedByIp);
    }
}
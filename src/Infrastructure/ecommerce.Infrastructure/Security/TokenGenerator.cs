using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Common;
using ecommerce.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ecommerce.Infrastructure.Security;
internal sealed class TokenGenerator : IJwtTokenGenerator, IRefreshTokenGenerator {
    private readonly JwtSettings jwtSettings;
    private readonly IDateTimeProvider dateTimeProvider;

    public TokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions) {
        this.dateTimeProvider = dateTimeProvider;
        this.jwtSettings = jwtOptions.Value;
    }

    public String GenerateJwtToken(UserAggregate user) {
        Claim[] claims = GetClaims(user);

        //roles.ForEach(role => claims.Add(new(ClaimTypes.Role, role)));
        //permissions.ForEach(permission => claims.Add(new("permissions", permission)));

        JwtSecurityToken securityToken = CreateSecurityToken(claims);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    private JwtSecurityToken CreateSecurityToken(Claim[] claims) {
        return new(
            issuer: this.jwtSettings.Issuer,
            audience: this.jwtSettings.Audience,
            claims: claims,
            expires: this.dateTimeProvider.UtcNow.AddMinutes(this.jwtSettings.ExpiryMinutes),
            signingCredentials: CreateCredentials());
    }

    public String GenerateRefreshToken() {
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        Byte[] randomBytes = new Byte[64];
        randomNumberGenerator.GetBytes(randomBytes);
        return BitConverter.ToString(randomBytes).Replace("-", String.Empty).ToLower();
    }

    private SigningCredentials CreateCredentials() {
        Byte[] keyBytes = Encoding.UTF8.GetBytes(this.jwtSettings.Secret);
        SymmetricSecurityKey securityKey = new(keyBytes);
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        return signingCredentials;
    }

    private static Claim[] GetClaims(UserAggregate user) {
        return [new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FullName.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.FullName.LastName),
            new Claim(ClaimTypes.Name, $"{user.FullName}")];
    }
}
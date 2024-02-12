namespace ecommerce.Infrastructure.Settings;
public sealed record JwtSettings {
    public const String SectionName = "JwtSettings";
    public String Secret { get; init; } = null!;
    public String Issuer { get; init; } = null!;
    public String Audience { get; init; } = null!;
    public Int32 ExpiryMinutes { get; init; }
    public Int32 RefreshTokenTTL { get; init; }
}
namespace ecommerce.Infrastructure.Settings;
internal sealed record SmtpSettings {
    public String Server { get; init; } = null!;
    public Int32 Port { get; init; }
    public String Username { get; init; } = null!;
    public String Password { get; init; } = null!;
}
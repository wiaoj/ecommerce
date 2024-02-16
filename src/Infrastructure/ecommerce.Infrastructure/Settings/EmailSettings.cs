namespace ecommerce.Infrastructure.Settings;
internal sealed record EmailSettings {
    public const String SectionName = nameof(EmailSettings);
    public Boolean EnableEmailNotifications { get; init; }
    public String DefaultFromEmail { get; init; } = null!;
    public SmtpSettings SmtpSettings { get; init; } = null!;
}
using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ecommerce.Infrastructure.Services;
internal sealed class EmailProvider : IEmailProvider {
    private readonly EmailSettings emailSettings;

    public EmailProvider(IOptions<EmailSettings> emailSettings) {
        this.emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(Email to, String subject, String body) {
        MailMessage mailMessage = new();

        mailMessage.To.Add(to.Value);


        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.From = new(this.emailSettings.DefaultFromEmail, "W-Commerce", System.Text.Encoding.UTF8);

        SmtpClient smtpClient = new() {
            Credentials = new NetworkCredential(this.emailSettings.SmtpSettings.Username, this.emailSettings.SmtpSettings.Password),
            Port = this.emailSettings.SmtpSettings.Port,
            EnableSsl = true,
            Host = this.emailSettings.SmtpSettings.Server
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}
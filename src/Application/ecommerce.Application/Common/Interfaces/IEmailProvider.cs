using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Application.Common.Interfaces;
public interface IEmailProvider {
    Task SendEmailAsync(Email to, String subject, String body);
}
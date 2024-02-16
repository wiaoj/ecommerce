using ecommerce.Application.Common.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using MediatR;

namespace ecommerce.Application.Features.Users.Events;
internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent> {
    private readonly IEmailProvider emailProvider;

    public UserCreatedDomainEventHandler(IEmailProvider emailProvider) {
        this.emailProvider = emailProvider;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken) {
        String emailSubject = "Hesabınız Oluşturuldu";
        String emailBody = $"Merhaba {notification.User.FullName}, hesabınız başarıyla oluşturuldu.";

        await this.emailProvider.SendEmailAsync(notification.User.Email, emailSubject, emailBody);
    }
}
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;

namespace ecommerce.Domain.Aggregates.UserAggregate;
public sealed class UserFactory : IUserFactory {
    private readonly IPasswordHasher passwordHasher;

    public UserFactory(IPasswordHasher passwordHasher) {
        this.passwordHasher = passwordHasher;
    }

    public UserAggregate Create(FullName fullName, Email email, PhoneNumber phoneNumber, Password password) {
        UserAggregate user = new(UserId.CreateUnique, fullName, email, phoneNumber, password, []);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user));
        return user;
    }

    public Email CreateEmail(String email) {
        return new(email);
    }

    public FullName CreateFullName(String firstName, String lastName) {
        return CreateFullName(firstName, null, lastName);
    }

    public FullName CreateFullName(String firstName, IEnumerable<String>? middleNames, String lastName) {
        return new(firstName, middleNames, lastName);
    }

    public Password CreatePassword(String password) {
        String hash = this.passwordHasher.HashPassword(password, out String salt);
        return new(hash, salt);
    }

    public PhoneNumber CreatePhoneNumber(String? phoneNumber) {
        return new(phoneNumber);
    }

    public RefreshToken CreateRefreshToken(String token,
                                           DateTime created,
                                           DateTime expires,
                                           String createdByIp) {
        return new(token, created, expires, createdByIp);
    }
}
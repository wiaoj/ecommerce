using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Common.Models;

namespace ecommerce.Domain.Aggregates.UserAggregate;
public sealed class UserAggregate : AggregateRoot<UserId, Guid> {
    private readonly List<RefreshToken> refreshTokens;
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Password Password { get; private set; }
    public IReadOnlyCollection<RefreshToken> RefreshTokens => this.refreshTokens.AsReadOnly();

    public UserAggregate() { }
    internal UserAggregate(UserId id,
                           FullName fullName,
                           Email email,
                           PhoneNumber phoneNumber,
                           Password password,
                           List<RefreshToken> refreshTokens) : base(id) {
        ArgumentNullException.ThrowIfNull(fullName);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(phoneNumber);
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(refreshTokens);

        this.FullName = fullName;
        this.Email = email;
        this.PhoneNumber = phoneNumber;
        this.Password = password;
        this.refreshTokens = refreshTokens;
    }

    public void UpdateFullName(FullName newFullName) {
        ArgumentNullException.ThrowIfNull(newFullName);
        this.FullName = newFullName;
        //RaiseDomainEvent(new UserFullNameUpdatedEvent(this.Id, newFullName));
    }

    public void UpdateEmail(Email newEmail) {
        ArgumentNullException.ThrowIfNull(newEmail);
        this.Email = newEmail;
        //RaiseDomainEvent(new UserEmailUpdatedEvent(this.Id, newEmail));
    }

    public void ConfirmEmail() {
        this.Email = this.Email.Confirm();
        RaiseDomainEvent(new UserEmailConfirmedEvent(this.Id));
    }

    public void UpdatePhoneNumber(PhoneNumber newPhoneNumber) {
        ArgumentNullException.ThrowIfNull(newPhoneNumber);
        this.PhoneNumber = newPhoneNumber;
        //RaiseDomainEvent(new UserPhoneNumberUpdatedEvent(this.Id, newPhoneNumber));
    }

    public void ConfirmPhoneNumber() {
        this.PhoneNumber = this.PhoneNumber.Confirm();
        RaiseDomainEvent(new UserPhoneNumberConfirmedEvent(this.Id));
    }

    public void AddRefreshToken(RefreshToken token) {
        if(token.IsExpired)
            throw new ArgumentException("Token is already expired");

        if(this.refreshTokens.Exists(rt => rt.Token == token.Token))
            throw new InvalidOperationException("This token is already added.");

        this.refreshTokens.Add(token);
    }

    public void UpdateRefreshToken(String expiredTokenValue, RefreshToken newToken) {
        RefreshToken? oldToken = this.refreshTokens.Find(refreshToken => refreshToken.Token == expiredTokenValue)
            ?? throw new KeyNotFoundException("The token to be updated was not found.");

        if(oldToken.IsExpired)
            throw new Exception("Old refresh token is expired.");
       
        this.refreshTokens.Remove(oldToken); 
        this.refreshTokens.Add(newToken);
    }

    public void RevokeRefreshToken(String revokeToken) {
        this.refreshTokens.RemoveAll(token => token.Token == revokeToken);
    }

    public void RevokeRefreshTokens() {
        this.refreshTokens.Clear();
        //RaiseDomainEvent(new UserRefreshTokensRevokedEvent(this.Id));
    }

    public override void Delete() {
        RaiseDomainEvent(new UserDeletedDomainEvent(this));
    }
}
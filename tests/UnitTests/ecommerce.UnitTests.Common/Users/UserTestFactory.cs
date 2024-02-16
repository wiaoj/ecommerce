using Bogus;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users.Extensions;

namespace ecommerce.UnitTests.Common.Users;
public static class UserTestFactory {
    private static readonly Faker<UserAggregate> userFaker = new();
    private static readonly Faker faker = new();
    public static UserAggregate CreateUser() {
        return userFaker.CreateUser().Generate();
    }

    public static UserAggregate CreateUser(UserId id,
                                           FullName fullName,
                                           Email email,
                                           PhoneNumber phoneNumber,
                                           Password password) {
        return new(id, fullName, email, phoneNumber, password, []);
    }

    public static UserId CreateUserId() {
        return new(faker.Random.Guid());
    }

    public static FullName CreateFullName() {
        return new(faker.Name.FirstName(), faker.Name.LastName());
    }

    public static FullName CreateFullName(String firstName, String lastName) {
        return new(firstName, lastName);
    }

    public static Password CreatePassword() {
        return new(faker.Internet.Password(), faker.Random.Guid().ToString());
    }

    public static Password CreatePassword(String password) {
        return new(password, faker.Random.Guid().ToString());
    }

    public static Email CreateEmail() {
        return new(faker.Internet.Email());
    }

    public static Email CreateEmail(String email) {
        return new(email);
    }

    public static PhoneNumber CreatePhoneNumber() {
        return new(faker.Phone.PhoneNumber());
    }

    public static PhoneNumber CreatePhoneNumber(String? phoneNumber) {
        return new(phoneNumber);
    }

    public static RefreshToken CreateRefreshToken() {
        return new(Guid.NewGuid().ToString(),
                   DateTime.UtcNow,
                   DateTime.UtcNow.AddMinutes(20),
                   faker.Internet.Ip());
    }
}
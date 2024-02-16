using Bogus;
using ecommerce.Domain.Aggregates.UserAggregate;

namespace ecommerce.UnitTests.Common.Users.Extensions;
internal static class FakerExtensions {
    public static Faker<UserAggregate> CreateUser(this Faker<UserAggregate> faker) {
        Func<Faker, UserAggregate> func = faker => new UserAggregate(
                UserTestFactory.CreateUserId(),
                UserTestFactory.CreateFullName(faker.Name.FirstName(), faker.Name.LastName()),
                UserTestFactory.CreateEmail(faker.Internet.Email()),
                UserTestFactory.CreatePhoneNumber(faker.Phone.PhoneNumber()),
                UserTestFactory.CreatePassword(faker.Internet.Password()),
                []
            );
        return faker.CustomInstantiator(func);
    }
}
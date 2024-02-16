using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate.Interfaces;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Users;

namespace ecommerce.Domain.UnitTests.Aggregates.Users.UserFactoryTests;
public partial class UserFactoryTests {
    private readonly IPasswordHasher passwordHasher;
    private readonly IUserFactory userFactory;
    public UserFactoryTests() {
        this.passwordHasher = Substitute.For<IPasswordHasher>();
        this.userFactory = new UserFactory(this.passwordHasher);
    }
}
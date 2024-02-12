using Bogus;
using ecommerce.Application.Features.Authentication.Commands.Register;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate;

namespace ecommerce.Application.UnitTests.Features.Authentication.Command.Register;
public sealed class RegisterCommandHandlerTestData : TheoryData<RegisterCommand> {
    public RegisterCommandHandlerTestData() {
        Add(CreateValidCommand());
    }

    private static RegisterCommand CreateValidCommand() {
    Faker<RegisterCommand> command = new Faker<RegisterCommand>()
            .CustomInstantiator(faker => new RegisterCommand(
                faker.Name.FirstName(),
                faker.Name.FirstName(),
                faker.Name.LastName(),
                faker.Internet.Email(),
                faker.Phone.PhoneNumber(),
                faker.Internet.Password()));
        return command.Generate();
    }
}
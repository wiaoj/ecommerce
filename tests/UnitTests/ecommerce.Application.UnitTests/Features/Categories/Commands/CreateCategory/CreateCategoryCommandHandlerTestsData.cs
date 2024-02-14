using Bogus;
using ecommerce.Application.Features.Categories.Commands.CreateCategory;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.CreateCategory;
public sealed class CreateCategoryCommandHandlerTestsData : TheoryData<CreateCategoryCommand> {
    public CreateCategoryCommandHandlerTestsData() {
        Add(CreateValidCommand());
        Add(CreateValidCommandWithParent());
    }

    private static CreateCategoryCommand CreateValidCommand() {
        Faker<CreateCategoryCommand> command = new Faker<CreateCategoryCommand>()
                .CustomInstantiator(faker => new CreateCategoryCommand(
                    Constants.Common.XRequestId,
                    null,
                    faker.Commerce.Categories(1)[0]));
        return command.Generate();
    }

    private static CreateCategoryCommand CreateValidCommandWithParent() {
        Faker<CreateCategoryCommand> command = new Faker<CreateCategoryCommand>()
                .CustomInstantiator(faker => new CreateCategoryCommand(
                    Constants.Common.XRequestId,
                    Category.ParentId,
                    faker.Commerce.Categories(1)[0]));
        return command.Generate();
    }
}
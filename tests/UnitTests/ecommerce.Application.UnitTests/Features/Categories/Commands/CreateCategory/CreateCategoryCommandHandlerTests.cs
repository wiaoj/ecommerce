using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Application.UnitTests.Features.Categories.Commands.Extensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.UnitTests.Common.Categories;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.CreateCategory;
[Collection("CategoryCollection")]
public class CreateCategoryCommandHandlerTests {
    private readonly CreateCategoryCommandHandler handler;
    private readonly ICategoryRepository categoryRepository;
    private readonly ICategoryFactory categoryFactory;

    public CreateCategoryCommandHandlerTests() {
        this.categoryRepository = Substitute.For<ICategoryRepository>();
        this.categoryFactory = Substitute.For<ICategoryFactory>();
        this.handler = new CreateCategoryCommandHandler(this.categoryFactory, this.categoryRepository);
    }

    [Theory]
    [ClassData(typeof(CreateCategoryCommandHandlerTestsData))]
    public async Task HandleCreateCategoryCommand_GivenValidCommand_ShouldCreateAndReturnCategory(
        CreateCategoryCommand command) {
        // Arrange
        CategoryAggregate expectedCategory = CategoryTestFactory.CreateCategory();
        this.categoryFactory.Create(Arg.Any<CategoryId?>(), Arg.Any<CategoryName>()).Returns(expectedCategory);

        // Act 
        CreateCategoryCommandResult result = await this.handler.Handle(command, CancellationToken.None);

        // Assert 
        result.VerifyCategoryResultFromCategory(expectedCategory);
        this.categoryFactory.Received(1).Create(Arg.Any<CategoryId?>(), Arg.Any<CategoryName>());
        await this.categoryRepository.Received(1).CreateAsync(Arg.Any<CategoryAggregate>(), Arg.Any<CancellationToken>());
    }
}
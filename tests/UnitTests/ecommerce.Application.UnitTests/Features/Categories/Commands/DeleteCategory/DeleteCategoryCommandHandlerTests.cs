using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Application.Features.Categories.Commands.DeleteCategory;
using ecommerce.Application.UnitTests.Features.Categories.Commands.TestUtils;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using Moq;

namespace ecommerce.Application.UnitTests.Features.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandlerTests {
    private readonly DeleteCategoryCommandHandler handler;
    private readonly Mock<ICategoryRepository> mockCategoryRepository;
    private readonly ICategoryFactory categoryFactory;

    public DeleteCategoryCommandHandlerTests() {
        this.mockCategoryRepository = new Mock<ICategoryRepository>();
        this.categoryFactory = new CategoryFactory();
        this.handler = new DeleteCategoryCommandHandler(this.mockCategoryRepository.Object);
    }

    [Theory]
    [MemberData(nameof(ValidDeleteCategoryCommands))]
    public async Task HandleDeleteCategoryCommand_WhenCategoryIsValid_ShouldDeleteCategory(DeleteCategoryCommand command) {
        //arrange 
        CategoryAggregate category = Category.CreateValidCategory();
        this.mockCategoryRepository.Setup(repo => repo.FindByIdAsync(CategoryId.Create(command.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        // Act 
        await this.handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert   
        this.mockCategoryRepository.Verify(
            repository => repository.DeleteAsync(category, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory]
    [MemberData(nameof(ValidDeleteCategoryCommands))]
    public async Task HandleDeleteCategoryCommand_WhenCategoryIsNotFound_ShouldThrowException(DeleteCategoryCommand command) {
        // Arrange
        this.mockCategoryRepository.Setup(repo => repo.FindByIdAsync(CategoryId.Create(command.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

 
        // Act & Assert
        await Assert.ThrowsAsync<CategoryNotFoundException>(() => this.handler.Handle(command, It.IsAny<CancellationToken>()));
    }

    public static IEnumerable<Object[]> ValidDeleteCategoryCommands() {
        yield return new[] { DeleteCategoryCommandUtils.CreateCommand() };
    }
}
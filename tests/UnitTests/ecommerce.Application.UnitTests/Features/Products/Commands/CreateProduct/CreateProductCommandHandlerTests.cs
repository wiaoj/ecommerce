using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Products.Commands.CreateProduct;
using ecommerce.Application.UnitTests.Features.Products.Commands.TestUtils;
using ecommerce.Application.UnitTests.TestUtils.Categories.Extensions;
using ecommerce.Application.UnitTests.TestUtils.Products.Extensions;
using ecommerce.Domain.Aggregates.ProductAggregate;
using Moq;

namespace ecommerce.Application.UnitTests.Features.Products.Commands.CreateProduct;
public sealed class CreateProductCommandHandlerTests
{
    private readonly CreateProductCommandHandler handler;
    private readonly Mock<IProductRepository> mockProductRepository;
    private readonly IProductFactory productFactory;

    public CreateProductCommandHandlerTests()
    {
        mockProductRepository = new Mock<IProductRepository>();
        productFactory = new ProductFactory();
        handler = new(productFactory, mockProductRepository.Object);
    }

    [Theory]
    [MemberData(nameof(ValidCreateProductCommands))]
    public async Task HandleCreateProductCommand_WhenProductIsValid_ShouldCreateAndReturnProduct(CreateProductCommand command)
    {
        // Act 
        CreateProductCommandResult result = await handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert 
        Assert.NotNull(result);
        result.ValidateCreatedFrom(command);
        mockProductRepository.Verify(
            repository => repository.CreateAsync(It.IsAny<ProductAggregate>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    public static IEnumerable<object[]> ValidCreateProductCommands()
    {
        yield return new[] { CreateProductCommandUtils.CreateCommand() };
    }
}
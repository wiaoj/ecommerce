using ecommerce.Application.Features.Products.Commands.CreateProduct;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;

namespace ecommerce.Application.Features.Products.MappingExtensions;
internal static class CreateProductExtension {
    public static CreateProductCommandResult ToCreateProductResponse(this ProductAggregate product) {
        return new(product.Id.Value);
    }

    public static ProductAggregate FromCreateProductCommand(this IProductFactory productFactory,
                                                            ICategoryFactory categoryFactory,
                                                            CreateProductCommand command) {
        CategoryId categoryId = categoryFactory.CreateId(command.CategoryId);
        ProductName name = productFactory.CreateProductName(command.Name);
        ProductDescription description = productFactory.CreateProductDescription(command.Description);
        ProductAggregate product = productFactory.Create(categoryId, name, description);
        IEnumerable<ProductVariantEntity> items = command.Items.Select(item => productFactory.CreateVariant(product.Id, item));
        product.AddVariant(items);
        return product;
    }

    private static ProductVariantEntity CreateVariant(this IProductFactory productFactory,
                                                      ProductId productId,
                                                      CreateProductItemCommand command) {

        return productFactory.CreateVariant(productId, command.Stock, command.Price, command.OptionIds);
    }
}
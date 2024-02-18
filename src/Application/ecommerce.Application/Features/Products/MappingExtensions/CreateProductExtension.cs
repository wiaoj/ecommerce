using ecommerce.Application.Features.Products.Commands.CreateProduct;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;

namespace ecommerce.Application.Features.Products.MappingExtensions;
internal static class CreateProductExtension {
    public static CreateProductCommandResponse ToCreateProductResponse(this ProductAggregate product) {
        return new(product.Id,
                   product.CategoryId,
                   product.Name.Value,
                   product.Description.Value,
                   product.Variants.Select(CreateProductItemCommandResponse).ToList());
    }

    private static CreateProductItemCommandResponse CreateProductItemCommandResponse(ProductVariantEntity item) {
        return new(item.Id.Value,
                   item.SKU,
                   item.Price,
                   item.OptionIds.Select(option => option.Value).ToList());
    }

    public static ProductAggregate FromCreateProductCommand(this IProductFactory productFactory, CreateProductCommand command) {
        ProductAggregate product = productFactory.Create(command.CategoryId, command.Name, command.Description);
        IEnumerable<ProductVariantEntity> items = command.Items.Select(item
            => productFactory.CreateVariant(product.Id,
                                            item.Stock,
                                            item.Price,
                                            item.OptionIds));
        product.AddVariant(items);
        return product;
    }
}
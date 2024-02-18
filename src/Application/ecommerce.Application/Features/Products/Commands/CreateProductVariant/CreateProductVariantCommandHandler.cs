using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Exceptions;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Products.Commands.CreateProductVariant;
internal sealed class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand> {
    private readonly IProductFactory productFactory;
    private readonly IProductRepository productRepository;

    public CreateProductVariantCommandHandler(IProductFactory productFactory,
                                              IProductRepository productRepository) {
        this.productFactory = productFactory;
        this.productRepository = productRepository;
    }

    public async Task Handle(CreateProductVariantCommand request, CancellationToken cancellationToken) {
        ProductAggregate? product = await this.productRepository.FindByIdAsync(request.ProductId, cancellationToken);

        if(product.IsNull())
            throw new ProductNotFoundException(request.ProductId);

        ProductVariantEntity productVariant = this.productFactory.CreateVariant(request.ProductId,
                                                                                request.Stock,
                                                                                request.Price,
                                                                                request.Options);

        product.AddVariant(productVariant);
        await this.productRepository.UpdateAsync(product, cancellationToken);
    }
}
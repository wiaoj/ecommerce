using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.ProductAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Entities;
using ecommerce.Domain.Exceptions;
using MediatR;

namespace ecommerce.Application.Features.Products.Commands.CreateProductVariant;
internal sealed class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, Unit> {
    private readonly IProductFactory productFactory;
    private readonly IProductRepository productRepository;
    private readonly IGuardClause guardClause;

    public CreateProductVariantCommandHandler(IProductFactory productFactory,
                                              IProductRepository productRepository,
                                              IGuardClause guardClause) {
        this.productFactory = productFactory;
        this.productRepository = productRepository;
        this.guardClause = guardClause;
    }

    public async Task<Unit> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken) {
        ProductAggregate? product = await this.productRepository.FindByIdAsync(request.ProductId, cancellationToken);

        this.guardClause.ThrowIfNull(product, new ProductNotFoundException(request.ProductId));

        ProductVariantEntity productVariant = this.productFactory.CreateVariant(request.ProductId,
                                                                                request.Stock,
                                                                                request.Price,
                                                                                request.Options);

        product.AddVariant(productVariant);
        await this.productRepository.UpdateAsync(product, cancellationToken);
        return Unit.Value;
    }
}
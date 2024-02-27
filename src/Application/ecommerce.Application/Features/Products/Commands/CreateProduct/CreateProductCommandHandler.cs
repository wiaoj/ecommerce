using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Products.MappingExtensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace ecommerce.Application.Features.Products.Commands.CreateProduct;
internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResult> {
    private readonly IProductFactory productFactory;
    private readonly ICategoryFactory categoryFactory;
    private readonly IProductRepository productRepository;
    public CreateProductCommandHandler(IProductFactory productFactory,
                                       IProductRepository productRepository,
                                       ICategoryFactory categoryFactory) {
        this.productFactory = productFactory;
        this.productRepository = productRepository;
        this.categoryFactory = categoryFactory;
    }

    public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
        ProductAggregate product = this.productFactory.FromCreateProductCommand(this.categoryFactory, request);
        await this.productRepository.CreateAsync(product, cancellationToken);
        return product.ToCreateProductResponse();
    }
}
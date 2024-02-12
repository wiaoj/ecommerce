using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Products.MappingExtensions;
using ecommerce.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace ecommerce.Application.Features.Products.Commands.CreateProduct;
internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse> {
    private readonly IProductFactory productFactory;
    private readonly IProductRepository productRepository;
    public CreateProductCommandHandler(IProductFactory productFactory, IProductRepository productRepository) {
        this.productFactory = productFactory;
        this.productRepository = productRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
        ProductAggregate product = this.productFactory.FromCreateProductCommand(request);
        await this.productRepository.CreateAsync(product, cancellationToken);
        return product.ToCreateProductResponse();
    }
}
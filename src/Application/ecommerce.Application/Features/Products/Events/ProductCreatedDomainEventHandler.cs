using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Products.Events;
internal sealed class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;

    public ProductCreatedDomainEventHandler(ICategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken) {
        return this.categoryRepository
            .FindByIdAsync(notification.Product.CategoryId, cancellationToken)
            .ContinueWith(async categoryTask => {
                CategoryAggregate? category = await categoryTask;

                if(category.IsNull())
                    throw new CategoryNotFoundException(notification.Product.CategoryId);

                category.AddProduct(notification.Product.Id);
                await this.categoryRepository.UpdateAsync(category, cancellationToken);
            }, cancellationToken);
    }
}
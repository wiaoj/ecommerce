using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.ProductAggregate.Events;
using MediatR;

namespace ecommerce.Application.Features.Products.Events;
internal sealed class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public ProductCreatedDomainEventHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
    }

    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken) {
        return this.categoryRepository
            .FindByIdAsync(notification.Product.CategoryId, cancellationToken)
            .ContinueWith(async categoryTask => {
                CategoryAggregate? category = await categoryTask;
                this.guardClause.ThrowIfNull(category, new CategoryNotFoundException(notification.Product.CategoryId));

                category.AddProduct(notification.Product.Id);
                await this.categoryRepository.UpdateAsync(category, cancellationToken);
            }, cancellationToken);
    }
}
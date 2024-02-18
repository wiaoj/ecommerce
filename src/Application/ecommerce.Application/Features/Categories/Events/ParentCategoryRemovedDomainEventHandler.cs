using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal sealed class ParentCategoryRemovedDomainEventHandler : INotificationHandler<ParentCategoryRemovedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;

    public ParentCategoryRemovedDomainEventHandler(ICategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public async Task Handle(ParentCategoryRemovedDomainEvent notification, CancellationToken cancellationToken) {
        CategoryAggregate? parentCategory = await this.categoryRepository.FindByIdAsync(notification.ParentId, cancellationToken);

        if(parentCategory.IsNull())
            throw new CategoryNotFoundException(notification.ParentId);

        parentCategory.RemoveSubcategory(notification.CategoryId);
    }
}
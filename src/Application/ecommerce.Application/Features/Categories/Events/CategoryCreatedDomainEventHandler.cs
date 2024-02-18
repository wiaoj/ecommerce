using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal class CategoryCreatedDomainEventHandler : INotificationHandler<CategoryCreatedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;

    public CategoryCreatedDomainEventHandler(ICategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public async Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken) {
        await UpdateParentCategoryIfExists(notification, cancellationToken);
    }

    private async Task UpdateParentCategoryIfExists(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken) {
        await AddChildCategoryToParent(notification.Category.ParentId, notification.Category.Id, cancellationToken);
    }

    private async Task AddChildCategoryToParent(CategoryId? parentId, CategoryId id, CancellationToken cancellationToken) {
        if(parentId is null)
            return;

        CategoryAggregate? parentCategory = await this.categoryRepository.FindByIdAsync(parentId, cancellationToken);

        if(parentCategory.IsNull())
            throw new CategoryNotFoundException(parentId);

        parentCategory.AddSubcategory(id);
        await this.categoryRepository.UpdateAsync(parentCategory, cancellationToken);
    }
}
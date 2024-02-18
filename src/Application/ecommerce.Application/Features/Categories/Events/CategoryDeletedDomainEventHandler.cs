using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal sealed class CategoryDeletedDomainEventHandler : INotificationHandler<CategoryDeletedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;

    public CategoryDeletedDomainEventHandler(ICategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public Task Handle(CategoryDeletedDomainEvent notification, CancellationToken cancellationToken) {
        Task[] tasks = [RemoveDeletedCategoryFromParent(notification, cancellationToken),
            DetachChildrenFromDeletedCategory(notification, cancellationToken)];
        return Task.WhenAll(tasks);
    }

    private async Task RemoveDeletedCategoryFromParent(CategoryDeletedDomainEvent notification, CancellationToken cancellationToken) {
        if(notification.Category.ParentId.IsNull())
            return;

        CategoryAggregate? parentCategory = await this.categoryRepository.FindByIdAsync(notification.Category.ParentId, cancellationToken);

        if(parentCategory.IsNull())
            throw new CategoryNotFoundException(notification.Category.ParentId);

        parentCategory.RemoveSubcategory(notification.Category.Id);
    }

    private async Task DetachChildrenFromDeletedCategory(CategoryDeletedDomainEvent notification, CancellationToken cancellationToken) {
        if(notification.Category.SubcategoryIds.Count.IsZero())
            return;

        List<CategoryAggregate> categories =
            await this.categoryRepository.FindCategoriesByParentIdAsync(notification.Category.Id, cancellationToken);

        categories.ForEach(category => {
            category.RemoveParentCategory();
        });
    }
}
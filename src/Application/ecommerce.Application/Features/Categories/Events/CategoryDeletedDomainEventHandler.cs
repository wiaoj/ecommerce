using ecommerce.Application.Common.Extensions;
using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal sealed class CategoryDeletedDomainEventHandler : INotificationHandler<CategoryDeletedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public CategoryDeletedDomainEventHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
    }

    public Task Handle(CategoryDeletedDomainEvent notification, CancellationToken cancellationToken) {
        Task[] tasks = [RemoveDeletedCategoryFromParent(notification, cancellationToken),
            DetachChildrenFromDeletedCategory(notification, cancellationToken)];
        return Task.WhenAll(tasks);
    }

    private async Task RemoveDeletedCategoryFromParent(CategoryDeletedDomainEvent notification, CancellationToken cancellationToken) {
        if(notification.Category.ParentId is null)
            return;

        CategoryAggregate? parentCategory = await this.categoryRepository.FindByIdAsync(notification.Category.ParentId, cancellationToken);
        this.guardClause.ThrowIfNull(parentCategory, new CategoryNotFoundException(notification.Category.ParentId));
        parentCategory.RemoveSubCategory(notification.Category.Id);
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
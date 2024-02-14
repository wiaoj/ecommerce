using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal class CategoryCreatedDomainEventHandler : INotificationHandler<CategoryCreatedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public CategoryCreatedDomainEventHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
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
        this.guardClause.ThrowIfNull(parentCategory, new CategoryNotFoundException(parentId));
        parentCategory.AddSubcategory(id);
        await this.categoryRepository.UpdateAsync(parentCategory, cancellationToken);
    }
}
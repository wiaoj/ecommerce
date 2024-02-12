using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal sealed class ParentCategoryChangedDomainEventHandler : INotificationHandler<ParentCategoryChangedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public ParentCategoryChangedDomainEventHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
    }

    public async Task Handle(ParentCategoryChangedDomainEvent notification, CancellationToken cancellationToken) {
        CategoryId parentCategoryId = CategoryId.Create(notification.ParentId);
        CategoryAggregate? parentCategory = await this.categoryRepository.FindByIdAsync(parentCategoryId, cancellationToken);
        this.guardClause.ThrowIfNull(parentCategory, new CategoryNotFoundException(parentCategoryId));
        parentCategory.AddChildCategory(notification.CategoryId);
    }
}
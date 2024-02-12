using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal sealed class ParentCategoryRemovedDomainEventHandler : INotificationHandler<ParentCategoryRemovedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public ParentCategoryRemovedDomainEventHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
    }

    public async Task Handle(ParentCategoryRemovedDomainEvent notification, CancellationToken cancellationToken) {
        CategoryAggregate? parentCategory = await this.categoryRepository.FindByIdAsync(notification.ParentId, cancellationToken);
        this.guardClause.ThrowIfNull(parentCategory, new CategoryNotFoundException(notification.ParentId));
        parentCategory.RemoveSubCategory(notification.CategoryId);
    }
}
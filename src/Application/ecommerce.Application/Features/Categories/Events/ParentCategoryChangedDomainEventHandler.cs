using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.Events;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Events;
internal sealed class ParentCategoryChangedDomainEventHandler : INotificationHandler<ParentCategoryChangedDomainEvent> {
    private readonly ICategoryRepository categoryRepository;
    private readonly ICategoryFactory categoryFactory;

    public ParentCategoryChangedDomainEventHandler(ICategoryRepository categoryRepository,
                                                   ICategoryFactory categoryFactory) {
        this.categoryRepository = categoryRepository;
        this.categoryFactory = categoryFactory;
    }


    public async Task Handle(ParentCategoryChangedDomainEvent notification, CancellationToken cancellationToken) {
        CategoryId parentCategoryId = this.categoryFactory.CreateId(notification.ParentId);
        CategoryAggregate? parentCategory = await this.categoryRepository.FindByIdAsync(parentCategoryId, cancellationToken);

        if(parentCategory.IsNull())
            throw new CategoryNotFoundException(parentCategoryId);

        parentCategory.AddSubcategory(notification.CategoryId);
    }
}
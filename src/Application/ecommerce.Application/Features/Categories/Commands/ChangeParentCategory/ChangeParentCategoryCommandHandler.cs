using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Features.Categories.Exceptions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
internal sealed class ChangeParentCategoryCommandHandler : IRequestHandler<ChangeParentCategoryCommand> {
    private readonly ICategoryRepository categoryRepository;
    private readonly ICategoryFactory categoryFactory;

    public ChangeParentCategoryCommandHandler(ICategoryRepository categoryRepository,
                                              ICategoryFactory categoryFactory) {
        this.categoryRepository = categoryRepository;
        this.categoryFactory = categoryFactory;
    }

    public async Task Handle(ChangeParentCategoryCommand request, CancellationToken cancellationToken) {
        CategoryId categoryId = this.categoryFactory.CreateId(request.Id);
        CategoryAggregate? category = await this.categoryRepository.FindByIdAsync(categoryId, cancellationToken);

        if(category.IsNull())
            throw new CategoryNotFoundException(request.Id);

        category.SetParentCategory(this.categoryFactory.CreateId(request.ParentCategoryId));
        await this.categoryRepository.UpdateAsync(category, cancellationToken);
    }
}
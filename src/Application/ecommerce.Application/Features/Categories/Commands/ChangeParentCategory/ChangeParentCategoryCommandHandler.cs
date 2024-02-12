using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
internal sealed class ChangeParentCategoryCommandHandler : IRequestHandler<ChangeParentCategoryCommand> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public ChangeParentCategoryCommandHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
    }
    
    public async Task Handle(ChangeParentCategoryCommand request, CancellationToken cancellationToken) {
        CategoryId categoryId = CategoryId.Create(request.Id);
        CategoryAggregate? category = await this.categoryRepository.FindByIdAsync(categoryId, cancellationToken);

        this.guardClause.ThrowIfNull(category, new CategoryNotFoundException(request.Id));

        category.SetParentCategory(CategoryId.Create(request.ParentCategoryId));
        await this.categoryRepository.UpdateAsync(category, cancellationToken);
    }
}
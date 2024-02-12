using ecommerce.Application.Common.Extensions;
using ecommerce.Application.Common.Guard;
using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Application.Features.Categories.MappingExtensions;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Queries.CategoryById;
internal sealed class CategoryByIdQueryHandler : IRequestHandler<CategoryByIdQuery, CategoryByIdResult> {
    private readonly ICategoryRepository categoryRepository;
    private readonly IGuardClause guardClause;

    public CategoryByIdQueryHandler(ICategoryRepository categoryRepository, IGuardClause guardClause) {
        this.categoryRepository = categoryRepository;
        this.guardClause = guardClause;
    }

    public async Task<CategoryByIdResult> Handle(CategoryByIdQuery request, CancellationToken cancellationToken) {
        CategoryId id = CategoryId.Create(request.Id);
        CategoryAggregate? category = await this.categoryRepository.FindByIdAsync(id, false, cancellationToken);
        this.guardClause.ThrowIfNull(category, new CategoryNotFoundException(id));
        CategoryByIdResult result = CategoryByIdResult.FromCategoryAggregate(category);

        if(category.SubcategoryIds.Count.IsNotZero()) {
            List<CategoryAggregate> children = await this.categoryRepository.FindCategoriesByParentIdAsync(id, false, cancellationToken);
            result.AddSubCategories(children);
        }

        return result;
    }
}
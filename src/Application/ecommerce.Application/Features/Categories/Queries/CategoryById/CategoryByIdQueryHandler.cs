using ecommerce.Application.Common.Repositories;
using ecommerce.Application.Exceptions.Categories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Extensions;
using MediatR;

namespace ecommerce.Application.Features.Categories.Queries.CategoryById;
internal sealed class CategoryByIdQueryHandler : IRequestHandler<CategoryByIdQuery, CategoryByIdResult> {
    private readonly ICategoryRepository categoryRepository;
    private readonly ICategoryFactory categoryFactory;

    public CategoryByIdQueryHandler(ICategoryRepository categoryRepository, ICategoryFactory categoryFactory) {
        this.categoryRepository = categoryRepository;
        this.categoryFactory = categoryFactory;
    }

    public async Task<CategoryByIdResult> Handle(CategoryByIdQuery request, CancellationToken cancellationToken) {
        CategoryId id = this.categoryFactory.CreateId(request.Id);
        CategoryAggregate? category = await this.categoryRepository.FindByIdAsync(id, false, cancellationToken);

        if(category.IsNull())
            throw new CategoryNotFoundException(id);

        CategoryByIdResult result = CategoryByIdResult.FromCategoryAggregate(category);

        if(category.SubcategoryIds.Count.IsNotZero()) {
            List<CategoryAggregate> children = await this.categoryRepository.FindCategoriesByParentIdAsync(id, false, cancellationToken);
            result.AddSubCategories(children);
        }

        return result;
    }
}
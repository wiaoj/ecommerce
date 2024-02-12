using ecommerce.Application.Common.Repositories;
using ecommerce.Domain.Aggregates.CategoryAggregate;
using MediatR;

namespace ecommerce.Application.Features.Categories.Queries.Categories;
internal sealed class CategoriesQueryHandler : IRequestHandler<CategoriesQuery, IEnumerable<CategoriesResult>> {
    private readonly ICategoryRepository categoryRepository;

    public CategoriesQueryHandler(ICategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoriesResult>> Handle(CategoriesQuery request, CancellationToken cancellationToken) {
        IEnumerable<CategoryAggregate> categories = await this.categoryRepository.FindAllAsync(cancellationToken);
        return categories.Select(CategoriesResult.FromCategoryAggregate);
    }
}
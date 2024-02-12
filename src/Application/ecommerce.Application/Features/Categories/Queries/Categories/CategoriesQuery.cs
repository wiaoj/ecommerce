using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Categories.Queries.Categories;
public sealed record CategoriesQuery() : IRequest<IEnumerable<CategoriesResult>>, IHasDistributedCache {
    public String CacheKey => Constants.Categories.CacheKeyRegistry;
}
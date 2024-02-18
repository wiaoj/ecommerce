using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Categories.Queries.CategoryById;
public sealed record CategoryByIdQuery(Guid Id) : IRequest<CategoryByIdResult>, IHasDistributedCache {
    public String CacheGroupKey => CategoryApplicationConstants.CacheGroupKeyRegistry;
}
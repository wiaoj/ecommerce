using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.DeleteCategory;
public sealed record DeleteCategoryCommand(Guid RequestId, Guid Id)
    : IRequest, IAuthorizeRequest, IHasTransaction, IHasEvent, IHasIdemponency, IHasCacheInvalidation {
    public String CacheGroupKey => CategoryApplicationConstants.CacheGroupKeyRegistry;

    public IEnumerable<String> Roles => ["Admin"];

    public IEnumerable<String> Permissions => [CategoryApplicationConstants.Permissions.Delete];
}

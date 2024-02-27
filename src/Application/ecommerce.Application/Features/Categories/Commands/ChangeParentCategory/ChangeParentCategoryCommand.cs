using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
public sealed record ChangeParentCategoryCommand(Guid Id, Guid ParentCategoryId)
    : IRequest, IAuthorizeRequest, IHasTransaction, IHasEvent, IHasCacheInvalidation {
    public String CacheGroupKey => CategoryApplicationConstants.CacheGroupKeyRegistry;

    public IEnumerable<String> Roles => ["Admin"];

    public IEnumerable<String> Permissions => [CategoryApplicationConstants.Permissions.Update];
}
using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.CreateCategory;
public sealed record CreateCategoryCommand(
    Guid? ParentCategoryId,
    String Name)
    : IRequest<CreateCategoryCommandResult>,
      IAuthorizeRequest,
      IHasTransaction,
      IHasEvent,
      IHasCacheInvalidation {
    public String CacheGroupKey => CategoryApplicationConstants.CacheGroupKeyRegistry;

    public IEnumerable<String> Roles => ["Admin"];

    public IEnumerable<String> Permissions => [CategoryApplicationConstants.Permissions.Create];
}
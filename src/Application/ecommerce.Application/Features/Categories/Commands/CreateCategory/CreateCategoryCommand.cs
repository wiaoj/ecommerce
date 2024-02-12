using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.CreateCategory;
public sealed record CreateCategoryCommand(
    Guid RequestId,
    Guid? ParentCategoryId,
    String Name)
    : IRequest<CreateCategoryCommandResult>,
      IAuthorizeRequest,
      IHasTransaction,
      IHasEvent,
      IHasIdemponency,
      IHasCacheInvalidation {
    public String CacheKey => Constants.Categories.CacheKeyRegistry;

    public IEnumerable<String> Roles => ["Admin"];

    public IEnumerable<String> Permissions => [Constants.Categories.Permissions.Create];
}
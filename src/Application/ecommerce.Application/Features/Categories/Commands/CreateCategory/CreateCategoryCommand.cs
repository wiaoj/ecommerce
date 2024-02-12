using ecommerce.Application.Common.Interfaces;
using MediatR;
using Microsoft.VisualBasic;

namespace ecommerce.Application.Features.Categories.Commands.CreateCategory;
public sealed record CreateCategoryCommand(
    Guid RequestId,
    Guid? ParentCategoryId,
    String Name) : IRequest<CreateCategoryCommandResult>, IHasTransaction, IHasEvent, IHasIdemponency, IHasCacheInvalidation {
    public String CacheKey => Constants.Categories.CacheKeyRegistry;
}
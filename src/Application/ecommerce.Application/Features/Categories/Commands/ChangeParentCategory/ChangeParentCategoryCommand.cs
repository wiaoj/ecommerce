using ecommerce.Application.Common.Interfaces;
using MediatR;

namespace ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
public sealed record ChangeParentCategoryCommand(Guid RequestId, Guid Id, Guid ParentCategoryId) 
    : IRequest, IHasTransaction, IHasEvent, IHasIdemponency;
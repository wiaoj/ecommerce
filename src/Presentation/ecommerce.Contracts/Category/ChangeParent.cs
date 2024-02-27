using ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
using ecommerce.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Contracts.Category;
public static class ChangeParent {
    public sealed record Request(Guid Id, Guid ParentId) : IExternalRequest<ChangeParentCategoryCommand> {
        public ChangeParentCategoryCommand ToCommand() {
            return new(this.Id, this.ParentId);
        }
    }
}
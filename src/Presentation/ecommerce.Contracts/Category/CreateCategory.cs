using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Contracts;
public partial class External {
    public sealed record CreateCategoryRequest(Guid? ParentCategoryId,
                                               String Name) : IExternalRequest<CreateCategoryCommand> {
        public CreateCategoryCommand ToCommand() {
            return new(this.ParentCategoryId, this.Name);
        }
    }
}
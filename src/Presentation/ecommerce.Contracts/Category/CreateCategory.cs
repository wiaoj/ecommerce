using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Contracts;
public partial class External {
    public sealed record CreateCategoryRequest([FromHeader(Name = "X-Request-Id")] Guid RequestId,
                                               Guid? ParentCategoryId,
                                               String Name) : IExternalRequest<CreateCategoryCommand> {
        public CreateCategoryCommand ToCommand() {
            return new(this.RequestId, this.ParentCategoryId, this.Name);
        }
    }

    public sealed record CreatedCategoryResponse(Guid Id,
                                                 String Name,
                                                 Guid? ParentCategoryId);
}
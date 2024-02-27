using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Contracts.Common;

namespace ecommerce.Contracts.Category;
public static class CreateCategory {
    public sealed record Request(Guid? ParentId, String Name) : IExternalRequest<CreateCategoryCommand> {
        public CreateCategoryCommand ToCommand() {
            return new(this.ParentId, this.Name);
        }
    }

    public sealed record Response(Guid Id) : IExternalResponse;

    public static Response CreateResponse(this CreateCategoryCommandResult result) {
        return new(result.Id);
    }
}
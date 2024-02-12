using ecommerce.Application.Features.Categories.Commands.DeleteCategory;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers.Categories.v1;
public partial class CategoriesController : BaseController {

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromHeader(Name = "X-Request-Id")] Guid requestId,
                                            [FromRoute] Guid id,
                                            CancellationToken cancellationToken) {
        DeleteCategoryCommand command = new(requestId, id);
        await this.Sender.Send(command, cancellationToken);
        return NoContent();
    }
}
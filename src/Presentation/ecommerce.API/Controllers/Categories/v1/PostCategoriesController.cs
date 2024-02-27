using ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Contracts.Category;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers.Categories.v1;
public partial class CategoriesController : BaseController {
    [HttpPost]
    [ProducesResponseType<CreateCategoryCommandResult>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateCategory.Request request, CancellationToken cancellationToken) {
        CreateCategoryCommand command = request.ToCommand();
        CreateCategoryCommandResult result = await this.Sender.Send(command, cancellationToken);
        CreateCategory.Response response = result.CreateResponse();
        return CreatedAtAction(nameof(this.GetById), new { response.Id }, response);
    }

    [HttpPost]
    [Route("{id:guid}/change-parent/{parentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeParentCategoryId([FromRoute] Guid id, [FromRoute] Guid parentId,
                                                            CancellationToken cancellationToken) {
        ChangeParentCategoryCommand command = new(id, parentId);
        await this.Sender.Send(command, cancellationToken);
        return NoContent();
    }
}
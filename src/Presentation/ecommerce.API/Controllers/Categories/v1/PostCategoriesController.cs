using ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using Microsoft.AspNetCore.Mvc;
using static ecommerce.Contracts.External;

namespace ecommerce.API.Controllers.Categories.v1;
public partial class CategoriesController : BaseController {
    [HttpPost]
    [ProducesResponseType<CreateCategoryCommandResult>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken) {
        CreateCategoryCommand command = request.ToCommand();
        CreateCategoryCommandResult result = await this.Sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(CategoriesController.GetById), new { result.Id }, result);
    }

    [HttpPost]
    [Route("{id:guid}/change-parent/{parentCategoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeParentCategoryId([FromRoute] Guid id,
                                                            [FromRoute] Guid parentCategoryId,
                                                            CancellationToken cancellationToken) {
        ChangeParentCategoryCommand command = new(id, parentCategoryId);
        await this.Sender.Send(command, cancellationToken);
        return NoContent();
    }
}
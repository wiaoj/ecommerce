using ecommerce.Application.Features.Categories.Commands.ChangeParentCategory;
using ecommerce.Application.Features.Categories.Commands.CreateCategory;
using ecommerce.Contracts.Category;
using Microsoft.AspNetCore.Mvc;
using static ecommerce.Contracts.External;

namespace ecommerce.API.Controllers.Categories.v1;
public partial class CategoriesController : BaseController {

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken) {
        CreateCategoryCommand command = request.ToCommand();
        CreateCategoryCommandResult commandResponse = await this.Sender.Send(command, cancellationToken);
        //TODO: Created Route eklenecek
        CreatedCategoryResponse externalResponse = commandResponse.ToResponse(); 
        return CreatedAtAction(nameof(CategoriesController.GetById), new { id = externalResponse.Id }, externalResponse);
    }

    [HttpPost]
    [Route("{id:guid}/change-parent/{parentCategoryId:guid}")]
    public async Task<IActionResult> ChangeParentCategoryId(
        [FromHeader(Name ="X-Request-Id")] Guid requestId,
        [FromRoute] Guid id,
                                                            [FromRoute] Guid parentCategoryId,
                                                            CancellationToken cancellationToken) {
        ChangeParentCategoryCommand command = new(requestId, id, parentCategoryId);
        await this.Sender.Send(command, cancellationToken);
        return NoContent();
    }
}
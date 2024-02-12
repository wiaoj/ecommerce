using ecommerce.Application.Features.Categories.Queries.Categories;
using ecommerce.Application.Features.Categories.Queries.CategoryById;
using ecommerce.Contracts.Category;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers.Categories.v1;
public partial class CategoriesController : BaseController {
    [HttpGet]
    [ProducesResponseType<IEnumerable<ExternalResponse.GetCategoriesResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
        CategoriesQuery query = new();
        IEnumerable<CategoriesResult> result = await this.Sender.Send(query, cancellationToken);
        IEnumerable<ExternalResponse.GetCategoriesResponse> response =
            result.Select(category => new ExternalResponse.GetCategoriesResponse(category.Id,
                                                                                 category.Name,
                                                                                 category.ParentId,
                                                                                 category.ChildIds));
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken) {
        CategoryByIdQuery query = new(id);
        CategoryByIdResult result = await this.Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
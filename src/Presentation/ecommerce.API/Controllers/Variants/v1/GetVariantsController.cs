using ecommerce.Application.Features.Variants.Queries.Variants;
using ecommerce.Application.Features.Variants.Queries.VariantsByCategoryId;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers.Variants.v1;
public partial class VariantsController : BaseController {
    [HttpGet]
    [Route("/api/v{version:apiVersion}/variants")]
    public async Task<IActionResult> GetVariants(CancellationToken cancellationToken) {
        VariantsQuery query = new();
        return Ok(await this.Sender.Send(query, cancellationToken));
    }

    [HttpGet]
    [Route("/api/v{version:apiVersion}/variants/{id}")]
    public async Task<IActionResult> GetVariantOptions([FromRoute] Guid id, CancellationToken cancellationToken) {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetVariantsByCategoryId([FromRoute] Guid categoryId, CancellationToken cancellationToken) {
        VariantsByCategoryIdQuery query = new(categoryId);
        return Ok(await this.Sender.Send(query, cancellationToken));
    }
}
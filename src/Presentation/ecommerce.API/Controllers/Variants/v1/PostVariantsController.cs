using ecommerce.Application.Features.Variants.Commands.CreateVariant;
using ecommerce.Application.Features.Variants.Commands.CreateVariantOption;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers.Variants.v1;
public partial class VariantsController : BaseController {
    [HttpPost]
    public async Task<IActionResult> CreateVariant([FromRoute] Guid categoryId, [FromBody] String name, CancellationToken cancellationToken) {
        CreateVariantCommand command = new(categoryId, name);
        await this.Sender.Send(command, cancellationToken);
        return Ok();
    } 
    
    [HttpPost]
    [Route("{variantId:guid}/options")]
    public async Task<IActionResult> CreateOption([FromRoute] Guid categoryId,
                                                  [FromRoute] Guid variantId,
                                                  [FromBody] String value,
                                                  CancellationToken cancellationToken) {
        CreateVariantOptionCommand command = new(categoryId, variantId,value);
        await this.Sender.Send(command, cancellationToken);
        return Ok();
    }
}
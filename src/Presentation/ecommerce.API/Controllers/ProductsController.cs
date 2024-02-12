using Asp.Versioning;
using ecommerce.Application.Features.Products.Commands.CreateProduct;
using ecommerce.Application.Features.Products.Commands.CreateProductVariant;
using ecommerce.Contracts.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static ecommerce.Contracts.External;

namespace ecommerce.API.Controllers;
[Route("api/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class ProductsController : ControllerBase {
    private readonly ISender sender;

    public ProductsController(ISender sender) {
        this.sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken) {
        CreateProductCommand command = request.ToCommand();
        CreateProductCommandResponse response = await this.sender.Send(command, cancellationToken);
        return Ok(response.ToResponse());
    }

    [HttpPost]
    [Route(nameof(CreateVariant))]
    public async Task<IActionResult> CreateVariant([FromBody] CreateProductVariantRequest request, CancellationToken cancellationToken) {
        CreateProductVariantCommand command = request.ToCommand();
        await this.sender.Send(command, cancellationToken);
        return Ok();
    }
}
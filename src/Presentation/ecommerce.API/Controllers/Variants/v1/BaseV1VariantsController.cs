using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers.Variants.v1;
[Route("api/v{version:apiVersion}/categories/{categoryId:guid}/variants")]
public partial class VariantsController : BaseController {
    public VariantsController(ISender sender) : base(sender) { }
}
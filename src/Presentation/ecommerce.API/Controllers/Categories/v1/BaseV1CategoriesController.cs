using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers.Categories.v1;
[Route("api/v{version:apiVersion}/categories")]
[ApiVersion("1.0")]
public partial class CategoriesController : BaseController {
    public CategoriesController(ISender sender) : base(sender) { }
}
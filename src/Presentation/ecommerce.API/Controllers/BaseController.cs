using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase {
    protected readonly ISender Sender;

    protected BaseController(ISender sender) {
        ArgumentNullException.ThrowIfNull(sender);
        this.Sender = sender;
    }
}

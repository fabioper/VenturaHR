using Microsoft.AspNetCore.Mvc;

namespace Webhooks.Controllers;

[ApiController]
[Route("webhooks")]
public class WebhooksController : ControllerBase
{
    [HttpPost("user-created")]
    public async Task<IActionResult> UserCreated()
    {
        var message = "user created";
        return Ok(message);
    }
}
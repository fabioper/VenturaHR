using Common.Events.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Webhooks.Models.DTO;

namespace Webhooks.Controllers;

[ApiController]
[Route("webhooks")]
public class WebhooksController : ControllerBase
{
    private readonly IPublishEndpoint _bus;

    public WebhooksController(IPublishEndpoint bus) => _bus = bus;

    [HttpPost("user-created")]
    public async Task<IActionResult> UserCreated([FromBody] UserCreatedDto dto)
    {
        await _bus.Publish(new UserCreated
        {
            Id = dto.Id,
            Email = dto.Email,
            DisplayName = dto.DisplayName,
            Role = dto.Role
        });

        return Ok();
    }
}
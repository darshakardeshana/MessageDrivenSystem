using MessageProducer.Domain.Entities;
using MessageProducer.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageProducer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IRabbitMqService _rabbitMqService;

    public MessageController(IRabbitMqService rabbitMqService)
    {
        _rabbitMqService = rabbitMqService;
    }

    [HttpPost]
    public async Task<IActionResult> Publish([FromBody] string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return BadRequest("Message cannot be empty.");
        }

        Message messageToPublish = new Message(message); 

        await _rabbitMqService.PublishAsync(messageToPublish);

        return Ok(new { Success = true, Message = "Message published successfully." });
    }
}

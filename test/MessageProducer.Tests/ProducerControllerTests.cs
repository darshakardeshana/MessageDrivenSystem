using MessageProducer.Infrastructure.Services;
using MessageProducer.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MessageProducer.Tests
{
    public class ProducerControllerTests
    {
        [Fact]
        public void ProcessMessage_Should_ProcessMessage()
        {
            var mockMessageQueueService = new Mock<IRabbitMqService>();
            var controller = new MessageController(mockMessageQueueService.Object);

            var result = controller.Publish("This is test message");

            Assert.IsType<Task<IActionResult>>(result);
        }
    }
}
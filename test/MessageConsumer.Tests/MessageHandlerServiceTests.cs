using MessageConsumer.Domain.Entities;
using MessageConsumer.Infrastructure.Services;
using MessageConsumer.Infrastructure.Services.Implementations;
using Moq;

namespace MessageConsumer.Tests
{
    public class MessageHandlerServiceTests
    {
        private readonly Mock<IResultProcessingService> _resultProcessingServiceMock;
        private readonly MessageHandlerService _messageHandlerService;

        public MessageHandlerServiceTests()
        {
            _resultProcessingServiceMock = new Mock<IResultProcessingService>();
            _messageHandlerService = new MessageHandlerService(_resultProcessingServiceMock.Object);
        }

        [Fact]
        public void ProcessMessage_ValidMessage_CallsResultProcessingServiceWithSuccessResult()
        {
            var message = new Message { Content = "This is a valid message." };

            _messageHandlerService.ProcessMessage(message);

            _resultProcessingServiceMock.Verify(
                x => x.AddMessageProcessingResult(It.Is<MessageProcessingResult>(result =>
                    result.Message == message &&
                    result.Status == Status.Success &&
                    string.IsNullOrEmpty(result.ErrorMessage) &&
                    result.ProcessingTimeInSec > 0)),
                Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("This message contains Error")]
        [InlineData("error in the content")]
        public void ProcessMessage_InvalidMessageContent_CallsResultProcessingServiceWithErrorResult(string invalidContent)
        {
            var message = new Message { Content = invalidContent };

            _messageHandlerService.ProcessMessage(message);

            _resultProcessingServiceMock.Verify(
                x => x.AddMessageProcessingResult(It.Is<MessageProcessingResult>(result =>
                    result.Message == message &&
                    result.Status == Status.Error &&
                    !string.IsNullOrEmpty(result.ErrorMessage) &&
                    result.ProcessingTimeInSec > 0)),
                Times.Once);
        }
    }
}
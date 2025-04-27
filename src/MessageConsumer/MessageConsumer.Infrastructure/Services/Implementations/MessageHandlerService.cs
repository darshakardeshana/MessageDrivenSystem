using MessageConsumer.Domain.Entities;

namespace MessageConsumer.Infrastructure.Services.Implementations
{
    public class MessageHandlerService : IMessageHandlerService
    {
        private readonly IResultProcessingService _resultProcessingService;

        public MessageHandlerService(IResultProcessingService resultProcessingService)
        {
            _resultProcessingService = resultProcessingService;
        }

        public void ProcessMessage(Message message)
        {
            var processingTimeInSec = GetMessageProcessingTimeInSec();
            try
            {
                if (string.IsNullOrEmpty(message.Content.Trim()) || message.Content.Contains("Error", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Invalid message");
                }

                MessageProcessingResult messageProcessingResult = MessageProcessingResult.BuildSuccessResult(message, processingTimeInSec);
                _resultProcessingService.AddMessageProcessingResult(messageProcessingResult);
            }
            catch (Exception ex)
            {
                MessageProcessingResult messageProcessingResult = MessageProcessingResult.BuildErrorResult(message, processingTimeInSec, ex.Message);
                _resultProcessingService.AddMessageProcessingResult(messageProcessingResult);
            }
        }

        private static int GetMessageProcessingTimeInSec()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }
}

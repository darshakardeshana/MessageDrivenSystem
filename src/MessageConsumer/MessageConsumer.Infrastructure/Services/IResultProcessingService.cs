using MessageConsumer.Domain.Entities;

namespace MessageConsumer.Infrastructure.Services;

public interface IResultProcessingService
{
    void AddMessageProcessingResult(MessageProcessingResult messageProcessingResult);
    List<MessageProcessingResult> GetFailedMessages();
    List<MessageProcessingResult> GetSuccessMessage();
    List<MessageProcessingResult> GetAllProcessedMessages();
}
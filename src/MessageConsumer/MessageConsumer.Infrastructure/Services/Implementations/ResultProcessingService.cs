using MessageConsumer.Domain.Entities;

namespace MessageConsumer.Infrastructure.Services.Implementations;

public class ResultProcessingService : IResultProcessingService
{
    private List<MessageProcessingResult> _messageProcessingResult = new List<MessageProcessingResult>();

    public void AddMessageProcessingResult(MessageProcessingResult messageProcessingResult)
    {
        _messageProcessingResult.Add(messageProcessingResult);
    }

    public List<MessageProcessingResult> GetAllProcessedMessages()
    {
        return _messageProcessingResult;
    }

    public List<MessageProcessingResult> GetFailedMessages()
    {
        return _messageProcessingResult.Where(_ => _.Status == Status.Error).ToList();
    }

    public List<MessageProcessingResult> GetSuccessMessage()
    {
        return _messageProcessingResult.Where(_ => _.Status == Status.Success).ToList();
    }
}
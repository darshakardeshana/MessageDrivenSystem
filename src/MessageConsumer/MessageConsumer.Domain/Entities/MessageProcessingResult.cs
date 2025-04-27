namespace MessageConsumer.Domain.Entities
{
    public class MessageProcessingResult
    {
        public Message Message { get; private set; }
        public Status Status { get; private set; }
        public int ProcessingTimeInSec { get; private set; }
        public string ErrorMessage { get; private set; }

        private MessageProcessingResult(Message message, Status status, int processingTime, string errorMessage)
        {
            Message = message;
            Status = status;
            ProcessingTimeInSec = processingTime;
            ErrorMessage = errorMessage;
        }

        public static MessageProcessingResult BuildSuccessResult(Message message, int processingTime)
        {
            return new MessageProcessingResult(message, Status.Success, processingTime, string.Empty);
        }

        public static MessageProcessingResult BuildErrorResult(Message message, int processingTime, string errorMessage)
        {
            return new MessageProcessingResult(message, Status.Error, processingTime, errorMessage);
        }
    }
}

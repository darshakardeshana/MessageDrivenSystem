namespace MessageConsumer.WebApi.Dtos
{
    public class ResultSummary
    {
        public int TotalMessagesProcessed { get; set; }
        public int TotalSuccessMessages { get; set; }
        public int TotalFailedMessages { get; set; }

        public ResultSummary(int totalMessagesProcessed, int totalSuccessMessages, int totalFailedMessages)
        {
            TotalMessagesProcessed = totalMessagesProcessed;
            TotalSuccessMessages = totalSuccessMessages;
            TotalFailedMessages = totalFailedMessages;
        }
    }
}

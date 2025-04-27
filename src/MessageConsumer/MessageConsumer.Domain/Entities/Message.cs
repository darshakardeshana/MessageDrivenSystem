namespace MessageConsumer.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

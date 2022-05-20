namespace Hermes.Api
{
    public class Message
    {
        public Message(string to, string from, string subject, string body, DateTime receivedTime)
        {
            this.To = to;
            this.From = from;
            this.Subject = subject;
            this.Body = body;
            this.ReceivedTime = receivedTime;
        }

        public string To { get; init; }
        public string From { get; init; }
        public string Subject { get; init; }
        public string Body { get; init; }
        public DateTime ReceivedTime { get; init; }
    }
}

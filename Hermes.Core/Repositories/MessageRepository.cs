using MimeKit;

namespace Hermes.Core.Repositories
{
    public class MessageRepository
    {
        private readonly List<Message> messages = new();

        public IEnumerable<Message>  Messages() => messages;

        public void Add(Message message) => messages.Add(message);

        public bool Contains(MimeMessage message) =>
            Contains(
                message.From.ToString(),
                message.To.ToString(),
                message.Subject,
                message.Body.ToString());

        public bool Contains(string from, string to, string subject, string body) =>
            messages.Any(message =>
                message.Value.From.ToString() == from &&
                message.Value.To.ToString() == to &&
                message.Value.Subject == subject &&
                message.Value.Body.ToString() == body
            );

        public void DeleteAll()
        {
            messages.Clear();
        }
    }
}

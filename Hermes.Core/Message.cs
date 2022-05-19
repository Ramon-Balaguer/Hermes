using MimeKit;

namespace Hermes.Core
{
    public class Message
    {
        public MimeMessage Value { get; set; } = null!;
        public DateTime ReceivedTime { get; set; }
    }
}

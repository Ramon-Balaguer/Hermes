using System.Buffers;
using Hermes.Core.Repositories;
using MimeKit;
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;

namespace Hermes.Core;

public class Messages : MessageStore
{
    private readonly MessageRepository messageRepository = new();

    public override async Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
    {
        await using var stream = new MemoryStream();

        var position = buffer.GetPosition(0);
        while (buffer.TryGet(ref position, out var memory))
        {
            await stream.WriteAsync(memory, cancellationToken);
        }

        stream.Position = 0;

        var messageValue = await MimeMessage.LoadAsync(stream, cancellationToken);
        var message = new Message() { Value = messageValue, ReceivedTime = DateTime.Now };
        messageRepository.Add(message);

        return SmtpResponse.Ok;
    }

    public IEnumerable<Message> Get()
    {
        return messageRepository.Messages();
    }

    public bool Contains(string from, string to, string subject, string body)
    {
        return messageRepository.Contains(from, to, subject, body);
    }

    public void DeleteAll()
    {
        messageRepository.DeleteAll();
    }
}
using FluentAssertions;
using Hermes.FluentAssertions;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Hermes.Core.Tests
{
    public class HermesServerShould : IDisposable
    {
        private readonly HermesServer hermesServer;
        private const int SmtpPort = 587;

        public HermesServerShould()
        {
            hermesServer = new HermesServer(SmtpPort);
        }

        [Fact]
        public async Task MessageSent()
        {
            hermesServer.Start();
            using var smtpClient = new SmtpClient();
            var message = CreateMessage();

            await SendMail(smtpClient, message);

            hermesServer.Shutdown();
            hermesServer.Should().MessageReceived(
                    message.From.ToString(),
                    message.To.ToString(),
                    message.Subject,
                    message.Body.ToString()
                    );
        }

        [Fact]
        public async Task DontHaveMessagesWhenWheDeleteAllMessages()
        {
            hermesServer.Start();
            using var smtpClient = new SmtpClient();
            var message = CreateMessage();
            await SendMail(smtpClient, message);
            hermesServer.Shutdown();

            hermesServer.DeleteAllMessages();

            hermesServer.ReceivedMessages().Should().HaveCount(0);
        }

        [Fact]
        public async Task ServerIsNotRespondingWhenServerIsDown()
        {
            hermesServer.Start();
            hermesServer.Shutdown();

            Func<Task> act = async () => await SendMail(new SmtpClient(), CreateMessage());

            await act.Should().ThrowAsync<System.Net.Sockets.SocketException>();
        }

        [Fact]
        public async Task ServerIsNotRespondingWhenServerWasNotStarted()
        {
            Func<Task> act = async () => await SendMail(new SmtpClient(), CreateMessage());

            await act.Should().ThrowAsync<System.Net.Sockets.SocketException>();
        }

        private static async Task SendMail(SmtpClient smtpClient, MimeMessage email)
        {
            await smtpClient.ConnectAsync("localhost", SmtpPort);
            await smtpClient.SendAsync(email);
            await smtpClient.DisconnectAsync(true);
        }

        private static MimeMessage CreateMessage()
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("from_address@example.com"));
            email.To.Add(MailboxAddress.Parse("to_address@example.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Example HTML Message Body</h1>" };
            return email;
        }

        public void Dispose()
        {
            hermesServer.Shutdown();
        }
    }
}
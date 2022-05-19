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

        public HermesServerShould()
        {
            hermesServer = new HermesServer(25);
        }

        [Fact]
        public async Task MessageSent()
        {
            hermesServer.Start();

            var message = await SendMail();

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
            _ = await SendMail();
            hermesServer.Shutdown();

            hermesServer.DeleteAllMessages();

            hermesServer.ReceivedMessages().Should().HaveCount(0);
        }

        [Fact]
        public async Task ServerIsNotRespondingWhenServerIsDown()
        {
            hermesServer.Start();
            hermesServer.Shutdown();

            Func<Task> act = async () => _ = await SendMail();

            await act.Should().ThrowAsync<System.Net.Sockets.SocketException>();
        }

        [Fact]
        public async Task ServerIsNotRespondingWhenServerWasNotStarted()
        {
            Func<Task> act = async () => _ = await SendMail();

            await act.Should().ThrowAsync<System.Net.Sockets.SocketException>();
        }

        private static async Task<MimeMessage> SendMail()
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("from_address@example.com"));
            email.To.Add(MailboxAddress.Parse("to_address@example.com"));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Example HTML Message Body</h1>" };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("localhost", 25);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            return email;
        }

        public void Dispose()
        {
            hermesServer.Shutdown();
        }
    }
}
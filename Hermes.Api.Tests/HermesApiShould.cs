using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using FluentAssertions;
using Newtonsoft.Json;

namespace Hermes.Api.Tests
{
    public class HermesApiShould
    {
        [Fact]
        public async void ListNewMail()
        {
            await using var api = new TestServer();
            var client = api.CreateClient();
            var mail = await SendMail();

            var result = await client.GetAsync("/api/messages");

            var contentJson = await result.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<Message>>(contentJson);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            content?.Count.Should().Be(1);
            var message = content?.First();
            message.Should().NotBeNull();
            message?.To.Should().BeEquivalentTo(mail.To.ToString());
            message?.From.Should().BeEquivalentTo(mail.From.ToString());
            message?.Subject.Should().BeEquivalentTo(mail.Subject);
            message?.Body.Should().BeEquivalentTo(mail.Body.ToString());
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
    }
}
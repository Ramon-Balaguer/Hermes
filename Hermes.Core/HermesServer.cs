﻿using MimeKit;
using SmtpServer;

namespace Hermes.Core
{
    public class HermesServer : IDisposable
    { 
        private readonly Messages messages = new();
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly SmtpServer.SmtpServer smtpServer;

        public HermesServer(params int[] ports)
        {
            var options = new SmtpServerOptionsBuilder()
                .ServerName("localhost")
                .Port(ports)
                .Build();

            var serviceProvider = new SmtpServer.ComponentModel.ServiceProvider();
            serviceProvider.Add(messages);
            smtpServer = new SmtpServer.SmtpServer(options, serviceProvider);
        }
        
        public void Start()
        {
            _ = smtpServer.StartAsync(cancellationTokenSource.Token);
        }

        public void Shutdown()
        {
            smtpServer.Shutdown();
            cancellationTokenSource.Cancel();
        }

        public IEnumerable<Message> ReceivedMessages()
        {
            return messages.Get();
        }

        public bool Contains(MimeMessage message)
        {
            return messages.Contains(message);
        }

        public bool Contains(string from, string to, string subject, string body)
        {
            return messages.Contains(from, to, subject, body);
        }

        public void Dispose()
        {
            smtpServer.Shutdown();
            cancellationTokenSource.Cancel();
        }

        public void DeleteAllMessages()
        {
            messages.DeleteAll();
        }
    }
}

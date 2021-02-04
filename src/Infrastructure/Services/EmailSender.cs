using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Configuration;
using Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SMTPConfiguration _smtpConfiguration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(SMTPConfiguration smtpConfiguration, ILogger<EmailSender> logger)
        {
            _smtpConfiguration = smtpConfiguration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string recipientMail, string subject, string message)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_smtpConfiguration.Mail);
            email.To.Add(MailboxAddress.Parse(recipientMail));
            email.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = message;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpConfiguration.Mail, _smtpConfiguration.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            _logger.LogInformation("Email sent: {RecipientMail} {@subject}",
                recipientMail, subject);
        }

        public async Task SendEmailAsync(string recipientMail, string subject, string templateCode, string message)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_smtpConfiguration.Mail);
            email.To.Add(MailboxAddress.Parse(recipientMail));
            email.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = message;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpConfiguration.Mail, _smtpConfiguration.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            _logger.LogInformation("Email sent: {RecipientMail} {@subject}",
                recipientMail, subject);
        }
    }
}

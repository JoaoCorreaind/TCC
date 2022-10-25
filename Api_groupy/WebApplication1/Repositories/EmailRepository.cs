using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using WebApplication1.Interfaces;
using WebApplication1.settings;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly GmailSettings _emailSettings;

        public EmailRepository(IOptions<GmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string emailDestinatario, string assunto, string mensagemTexto, string mensagemHtml)
        {
            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            mensagem.To.Add(MailboxAddress.Parse(emailDestinatario));
            mensagem.Subject = assunto;
            var builder = new BodyBuilder { TextBody = mensagemTexto, HtmlBody = mensagemHtml };
            mensagem.Body = builder.ToMessageBody();
            try
            {
                var smtpClient = new SmtpClient();
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(_emailSettings.ServerAddress, _emailSettings.ServerDoor).ConfigureAwait(false);
                await smtpClient.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password).ConfigureAwait(false);
                await smtpClient.SendAsync(mensagem).ConfigureAwait(false);
                await smtpClient.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
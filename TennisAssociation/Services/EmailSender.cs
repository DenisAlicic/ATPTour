using System.Net;
using System.Net.Mail;
using TennisAssociation.Interfaces;
using TennisAssociation.Utils;

namespace TennisAssociation.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        private const string From = "tennisassociationrs2@gmail.com";
        private const string Pass = "TennisAssociation2020";
        private const string Host = "smtp.gmail.com";
        private const int Port = 587;

        public EmailSender()
        {
            _smtpClient = new SmtpClient(Host, Port)
            {
                Credentials = new NetworkCredential(From, Pass),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
        }

        public void Send(string to, Email mail)
        {
            MailMessage mailMessage = new MailMessage {From = new MailAddress(From, "Tennis Assocation app")};

            mailMessage.To.Add(to);
            mailMessage.Body = mail.Body;
            mailMessage.Subject = mail.Subject;
            
            _smtpClient.Send(mailMessage);
        }
    }
}
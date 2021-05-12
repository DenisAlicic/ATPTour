using System.Net;
using System.Net.Mail;
using TennisAssociation.Interfaces;
using TennisAssociation.Utils;

namespace TennisAssociation.Services
{
    /// <summary>
    /// Service which provide sending mails from default.
    /// application mail account.
    /// </summary>
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
        
        /// <summary>
        /// Send mails from default application mail account to users.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="mail"></param>
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
using TennisAssociation.Utils;

namespace TennisAssociation.Interfaces
{
    /// <summary>
    /// Interface for sending mails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Send emails
        /// </summary>
        /// <param name="to"></param>
        /// <param name="mail"></param>
        public void Send(string to, Email mail);
    }
}
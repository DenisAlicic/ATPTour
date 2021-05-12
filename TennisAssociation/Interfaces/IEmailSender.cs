using TennisAssociation.Utils;

namespace TennisAssociation.Interfaces
{
    public interface IEmailSender
    {
        public void Send(string to, Email mail);
    }
}
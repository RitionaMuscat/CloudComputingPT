using CloudComputingPT.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CloudComputingPT.DataAccess.Interfaces
{
    public interface IPubSubAccess
    {
        Task<string> PublishEmailAsync(MailMessage mail);
        Task<MailMessageAckId> ReadEmail();
        void AcknowledgeMessage(string ackId);
    }
}

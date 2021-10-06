using CloudComputingPT.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CloudComputingPT.DataAccess.Interfaces
{
    public interface IPubSubAccess
    {
        Task<string> PublishEmailAsync(MyMailMessage mail);
        Task<MailMessageAckId> ReadEmail();
        void AcknowledgeMessage(string ackId);
    }
}

using CloudComputingPT.DataAccess.Repositories;
using CloudComputingPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CloudComputingPT.DataAccess.Interfaces
{
    public interface IPubSubAccess
    {
        Task<string> PublishEmailAsync(MailMessage mail);
       Task< MailMessageAckId> ReadEmail();

    }
}

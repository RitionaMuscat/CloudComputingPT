using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CloudComputingPT.Models
{
    public class MailMessageAckId
    {
        public MailMessage MM { get; set; }
        public string AckId { get; set; }
    }
}

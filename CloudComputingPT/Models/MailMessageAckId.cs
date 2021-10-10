namespace CloudComputingPT.Models
{
    public class MailMessageAckId
    {
        public MyMailMessage MM { get; set; }
        public string AckId { get; set; }
    }
    public class MyMailMessage
    {
        public string To { get; set; }
        public string Body { get; set; }
   
    }
}

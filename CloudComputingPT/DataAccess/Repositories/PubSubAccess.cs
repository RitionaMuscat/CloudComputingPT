using CloudComputingPT.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using System.Net.Mail;
using Newtonsoft.Json;
using Google.Protobuf;
using System.Threading;
using CloudComputingPT.Models;

namespace CloudComputingPT.DataAccess.Repositories
{
 

    public class PubSubAccess : IPubSubAccess
    {
        private string projectId;
        public PubSubAccess(IConfiguration config)
        {
            projectId = config.GetSection("ProjectId").Value;
        }

        public async Task<string> PublishEmailAsync(MailMessage mail)
        {
            TopicName topic = new TopicName(projectId, "myQueue");
            PublisherClient client = await PublisherClient.CreateAsync(topic);
            string mail_serialized = JsonConvert.SerializeObject(mail);
            PubsubMessage message = new PubsubMessage
            {
                Data = ByteString.CopyFromUtf8(mail_serialized)
            };
            return await client.PublishAsync(message);

     
    }

        public async Task<MailMessageAckId> ReadEmail()
        {
            SubscriptionName subName = new SubscriptionName(projectId, "myQueue-sub");
            SubscriberServiceApiClient subscriberClient = SubscriberServiceApiClient.Create();
            MailMessageAckId mailMessageAckId = null;
            int messageCount = 0;
            MailMessage mm = null;
            try
            {
                PullResponse response = await subscriberClient.PullAsync(subName, returnImmediately: true, maxMessages: 1);
                if(response.ReceivedMessages.Count>0)
                {
                    string text = System.Text.Encoding.UTF8.GetString(response.ReceivedMessages[0].Message.Data.ToArray());
                    mm = JsonConvert.DeserializeObject<MailMessage>(text);
                    mailMessageAckId = new MailMessageAckId
                    {
                        MM = mm,
                        AckId = response.ReceivedMessages[0].AckId
                    };
                }
                foreach (ReceivedMessage msg in response.ReceivedMessages)
                {
                    string text = System.Text.Encoding.UTF8.GetString(msg.Message.Data.ToArray());
                    Console.WriteLine($"Message: {msg.Message.MessageId}: {text}");
                    Interlocked.Increment(ref messageCount);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return mailMessageAckId;
        }
    }
}


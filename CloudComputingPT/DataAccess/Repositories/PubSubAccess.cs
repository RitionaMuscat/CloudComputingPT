using CloudComputingPT.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using System.Net.Mail;
using Newtonsoft.Json;
using Google.Protobuf;
using CloudComputingPT.Models;
using Grpc.Core;

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

            MailMessageAckId mmWithAckId = null;
            try
            {
                // Pull messages from server,
                // allowing an immediate response if there are no messages.
                PullResponse response = await subscriberClient.PullAsync(subName, returnImmediately: true, maxMessages: 1);
                // Print out each received message.

                if (response.ReceivedMessages.Count > 0)
                {
                    try
                    {

                        var text = response.ReceivedMessages[0].Message;


                        //var mm = JsonConvert.DeserializeObject<MyMailMessage>(text);
                        var mm =  JsonConvert.DeserializeObject<MyMailMessage>(text.ToString());
                     
                        mmWithAckId = new MailMessageAckId
                        {
                            MM = mm,
                            AckId = response.ReceivedMessages[0].AckId
                        };
                    }
                    catch(JsonException EX )
                    {
                        System.Console.WriteLine(EX.Message.ToString());
                    }
                }
                else return null;
            }
            catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.Unavailable)
            {
                // UNAVAILABLE due to too many concurrent pull requests pending for the given subscription.
            }

            return mmWithAckId;

        }

        public void AcknowledgeMessage(string ackId)
        {
            SubscriptionName subName = new SubscriptionName(projectId, "myQueue-sub");
            SubscriberServiceApiClient subscriberClient = SubscriberServiceApiClient.Create();
            try
            {
                subscriberClient.Acknowledge(subName, new List<string>() { ackId });
            }
            catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.Unavailable)
            {
                // UNAVAILABLE due to too many concurrent pull requests pending for the given subscription.
            }


        }
    }
}


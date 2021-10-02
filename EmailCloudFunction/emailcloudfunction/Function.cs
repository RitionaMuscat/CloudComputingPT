using CloudNative.CloudEvents;
using Google.Cloud.Functions.Framework;
using Google.Events.Protobuf.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace emailcloudfunction
{
    
  
    /// <summary>
    /// A function that can be triggered in responses to changes in Google Cloud Storage.
    /// The type argument (StorageObjectData in this case) determines how the event payload is deserialized.
    /// The function must be deployed so that the trigger matches the expected payload type. (For example,
    /// deploying a function expecting a StorageObject payload will not work for a trigger that provides
    /// a FirestoreEvent.)
    /// </summary>
    public class Function : ICloudEventFunction<MessagePublishedData>
    {
        private readonly ILogger _logger;

        public Function(ILogger<Function> logger) =>
            _logger = logger;

        public Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
        {
            string nameFromMessage = data.Message?.TextData;

            dynamic deserializeObject = JsonConvert.DeserializeObject(nameFromMessage);

          
            string to = deserializeObject.To.ToString();
            string body= deserializeObject.From.ToString();


          //  string name = string.IsNullOrEmpty(nameFromMessage) ? "world" : nameFromMessage;
            _logger.LogInformation("To {to}", to);
            _logger.LogInformation("body {body}", body);

            return Task.CompletedTask;
        }
    }
}

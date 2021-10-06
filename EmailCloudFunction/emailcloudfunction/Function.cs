using CloudNative.CloudEvents;
using Google.Cloud.Functions.Framework;
using Google.Events.Protobuf.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System;
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
            /*
            string nameFromMessage = data.Message?.TextData;

            dynamic deserializeObject = JsonConvert.DeserializeObject(nameFromMessage);

          
            string to = deserializeObject.To.ToString();
            string body= deserializeObject.From.ToString();
            _logger.LogInformation("Before Request", to);
     
RestClient client = new RestClient ();
        client.BaseUrl = new Uri ("https://api.mailgun.net/v3");
               _logger.LogInformation("After URI", body);
        client.Authenticator =
            new HttpBasicAuthenticator ("api",
                                        "da1f8f04d4c041575309330398eb339b-dbdfb8ff-157f6229");
        RestRequest request = new RestRequest ();
        request.AddParameter ("domain", "sandboxf99c41093c6f4d7eb96dc8b82b260273.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter ("from", "schooldemo21@gmail.com");
        request.AddParameter ("to", to);

        request.AddParameter ("subject",deserializeObject.subject.ToString());
        request.AddParameter ("text", body);
        request.Method = Method.POST;
        var result = client.Execute (request);
          //  string name = string.IsNullOrEmpty(nameFromMessage) ? "world" : nameFromMessage;
            _logger.LogInformation("To {to}", to);
            _logger.LogInformation("body {body}", body);
        _logger.LogInformation("Logs Rit: ", result.StatusCode.ToString());
            return Task.CompletedTask;
            */



            string nameFromMessage = data.Message?.TextData;

            dynamic deserializedObject = JsonConvert.DeserializeObject(nameFromMessage);

            string to = deserializedObject.To.ToString();
            string body = deserializedObject.Body.ToString();

            _logger.LogInformation("To: {to}", to);
            _logger.LogInformation("Body: {body}", body);
            //send out an email
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                                "da1f8f04d4c041575309330398eb339b-dbdfb8ff-157f6229");
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "https://api.mailgun.net/v3/sandboxf99c41093c6f4d7eb96dc8b82b260273.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "schooldemo21@gmail.com");
                request.AddParameter("to", to);

                request.AddParameter("subject", "Receipt of service");
                request.AddParameter("text", body);
                request.Method = Method.POST;

                var result = client.Execute(request);
                _logger.LogInformation(result.StatusCode.ToString());
                _logger.LogInformation("Email sent");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Email not sent", ex.Message);
            }

            // string name = string.IsNullOrEmpty(nameFromMessage) ? "world" : nameFromMessage;
            return Task.CompletedTask;
        }
    }
}

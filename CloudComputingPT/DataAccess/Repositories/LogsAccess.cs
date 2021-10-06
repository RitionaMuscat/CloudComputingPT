using CloudComputingPT.DataAccess.Interfaces;
using Google.Api;
using Google.Cloud.Logging.Type;
using Google.Cloud.Logging.V2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputingPT.DataAccess.Repositories
{
    public class LogsAccess : ILogAccess
    {
        private string projectId;
        public LogsAccess(IConfiguration config)
        {
            projectId = config.GetSection("ProjectId").Value;
        }
        public void Log(string message)
        {

            var client = LoggingServiceV2Client.Create();
            LogName logName = new LogName(projectId, "mainapplication");
            LogEntry logEntry = new LogEntry
            {
                LogName = logName.ToString(),
                Severity = LogSeverity.Info,
                TextPayload = $"{message}"
            };
            MonitoredResource resource = new MonitoredResource { Type = "global" };

            client.WriteLogEntries(logName, resource, null,
                new[] { logEntry });

        }
    }
}

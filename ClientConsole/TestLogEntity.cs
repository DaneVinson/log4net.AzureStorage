using log4net.AzureStorage;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    /// <summary>
    /// Ultra-simplistic implementation of BaseLogEntity for testing purposes.
    /// </summary>
    public class TestLogEntity : BaseLogEntity
    {
        public TestLogEntity() { }

        public TestLogEntity(LoggingEvent loggingEvent) : base(loggingEvent)
        {
            // Vary PartionKen and RowKey from defaults.
            this.PartitionKey = DateTime.Now.Year.ToString();
            this.RowKey = Guid.NewGuid().ToString();

            this.LogMessage = loggingEvent.RenderedMessage;
        }


        public string LogMessage { get; set; }
    }
}

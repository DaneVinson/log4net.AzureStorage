using log4net.Appender;
using log4net.Core;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace log4net.AzureStorage
{
    /// <summary>
    /// Log4net appender used to write log entries to an Azure storage table.
    /// </summary>
    public class AzureStorageTableAppender : AppenderSkeleton
    {
        /// <summary>
        /// Override of the log4net Append event to write the log to an Azure storage table.
        /// </summary>
        /// <param name="loggingEvent"></param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Get the storage account and then the table creating it if necessary.
            var storageAccount = CloudStorageAccount.Parse(StaticConfig.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(StaticConfig.TableName);
            table.CreateIfNotExists();

            // Create the log entity, execute and insert operation on the table and handle results.
            BaseLogEntity logEntity = Activator.CreateInstance(StaticConfig.LogEntityType, loggingEvent) as BaseLogEntity;
            var result = table.Execute(TableOperation.Insert(logEntity));
            logEntity.AfterInsert(result);
        }
    }
}

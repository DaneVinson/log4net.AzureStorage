## log4net.AzureStorage ##
The log4net.AzureStorage project is a .NET 4.5 library conceived as a set of log4net appenders which implement persistence through the use of Azure Storage services. To utilize log4net.AzureStorage you should be familiar with the basics of logging with log4net ([https://www.nuget.org/packages/log4net/](https://www.nuget.org/packages/log4net/ "NuGet log4net")).

## Quick Start for Logging to Azure Storage Table ##
Install the NuGet package `log4net.AzureStorage` ([https://www.nuget.org/packages/log4net.AzureStorage/](https://www.nuget.org/packages/log4net.AzureStorage/ "NuGet log4net.AzureStorage")) to your project. 

Add the following to `appSettings` in your application's configuration file updating `AccountName` and `AccountKey` values with evidence from your Azure Storage account.

	<appSettings>
		<add key="AzureStorageLogTableConnectionString" value="DefaultEndpointsProtocol=https;AccountName={storage account name};AccountKey={key}" />
	</appSettings>

Add an `appender` element named `AzureStorageTableAppender` to `log4net`, `root` element in your application's configuration file.

	<log4net>
		<root>
			<appender name="AzureStorageTableAppender" type="log4net.AzureStorage.AzureStorageTableAppender, log4net.AzureStorage"></appender>
		</root>
	</log4net>

## Default Implementation ##
Using the settings in the Quick Start all log4net logging events will be written to a table in the specified Azure Storage account. By default the table will be named "LogTable" and will be created if it does not exist. The default table entity used (`SimpleLogEntity`) produces an Azure Storage Table with schema as follows.

	{
		PartitionKey,	// Defaults to UTC YYYY.MM
		RowKey,			// Defaults to UTC now + GUID
		Level,			// log4net logging level, e.g. INFO, ERROR
		Logger,			// Name of the log4net logger
		Message,		// Log message
		StamUtc			// DateTime stamp in UTC
	}

## log4net.AzureStorage ##
The log4net.AzureStorage project is a .NET 4.5 library conceived as a set of log4net appenders which implement persistence through the use of Azure Storage services. To utilize log4net.AzureStorage you should be familiar with the basics of logging with log4net ([https://www.nuget.org/packages/log4net/](https://www.nuget.org/packages/log4net/ "NuGet log4net")).

## Quick Start for Logging to Azure Storage Table ##
Install the NuGet package `log4net.AzureStorage` ([https://www.nuget.org/packages/log4net.AzureStorage/](https://www.nuget.org/packages/log4net.AzureStorage/ "NuGet log4net.AzureStorage")) to your project. 

Add the following to `appSettings` in your application's configuration file updating `AccountName` and `AccountKey` values with evidence from your Azure Storage account.

	<appSettings>
		<add key="AzureStorageLogTableConnectionString" value="DefaultEndpointsProtocol=https;AccountName={storage account name};AccountKey={key}" />
	</appSettings>

Add an `appender` element named "AzureStorageTableAppender" to `log4net`, `root` element in your application's configuration file.

	<log4net>
		<root>
			<appender name="AzureStorageTableAppender" type="log4net.AzureStorage.AzureStorageTableAppender, log4net.AzureStorage"></appender>
		</root>
	</log4net>

## Default Implementation ##
Using the settings in the Quick Start all log4net logging events will be written to a table in the specified Azure Storage account. By default the table will be named "LogTable" and will be created if it does not exist. The default table entity used (`SimpleLogEntity`) produces an Azure Storage Table with schema as follows.

	{
		PartitionKey,	// Set to UTC YYYY.MM
		RowKey,			// Set to UTC now + GUID
		Level,			// log4net logging level, e.g. INFO, ERROR
		Logger,			// Name of the log4net logger
		Message,		// Log message
		StamUtc			// DateTime stamp in UTC
	}

## Changing the Log Table Name ##
To change the name of the Azure Storage Table from the default "LogTable" simply add the following to `appSettings` in your application's configuration file. Note the value must be alphanumeric and contain no spaces.

	<appSettings>
		<add key="AzureStorageLogTableName" value="MyOwnTableName" />
	</appSettings>

## Custom Log Table Schema ##
By default the `AzureStorageTableAppender` uses the class `log4net.AzureStorage.SimpleLogEntity` to perform logging. The log entity class defines the schema of the Azure Storage Table that is created and used for logging. To create your own custom log table schema simple define a class which inherits `log4net.AzureStorage.BaseLogEntity` and add implementations for both constructors on the derived class. The first is the parameter-less default constructor required to perform actions against `Microsoft.WindowsAzure.Storage.Table.CloudTable`. The second should accept a `log4net.Core.LoggingEvent` argument and use that argument to set all desired properties on your derived log entity. Any public property with both a getter and setter will be added as a column in the Azure Storage Table. If the PartitionKey and RowKey properties are not assigned in the constructor those assignments will fall back to the defaults, i.e. UTC YYYY.MM and UTC now + GUID respectively. To use your custom log entity simply add the following to `appSettings` in your application's configuration file.

	<appSettings>
		<!--value should be of the form "{full class name},{assembly name}"-->
		<add key="LogEntityType" value="MyNamespace.MyCustomEntityClass,MyAssembly" />
	</appSettings>

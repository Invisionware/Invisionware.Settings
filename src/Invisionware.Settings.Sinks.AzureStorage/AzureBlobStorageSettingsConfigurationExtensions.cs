using System;
using Invisionware.Settings.Sinks.Azure;

namespace Invisionware.Settings.Sinks
{
	public static class AzureBlobSettingsConfigurationExtensions
	{
		/// <summary>
		/// Creates the Azures File Storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureBlobStorage:ConnectionString
		///		settings:sink:AzureBlobStorage:ContainerName
		///		settings:sink:AzureBlobStorage:FileName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration AzureFileStorage(this SettingsReaderSinkConfiguration settingsConfig) 
		{
			return AzureBlobStorage(settingsConfig,
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureBlobStorage:ConnectionString"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureBlobStorage:ContainerName"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureBlobStorage:FileName"]
			);
		}

		public static SettingsConfiguration AzureBlobStorage(this SettingsReaderSinkConfiguration settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json")
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (string.IsNullOrEmpty(containerName)) containerName = "AppSettings";
			if (string.IsNullOrEmpty(fileName)) fileName = "SEttings.json";

			return settingsConfig.Sink(
				new AzureBlobStorageSink(
					connectionString,
					containerName,
					fileName
					));
		}

		/// <summary>
		/// Creates the Azures File Storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureBlobStorage:ConnectionString
		///		settings:sink:AzureBlobStorage:ContainerName
		///		settings:sink:AzureBlobStorage:FileName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration AzureFileStorage(this SettingsWriterSinkConfiguration settingsConfig) 
		{
			return AzureBlobStorage(settingsConfig,
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureBlobStorage:ConnectionString"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureBlobStorage:ContainerName"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureBlobStorage:FileName"]
			);
		}

		public static SettingsConfiguration AzureBlobStorage(this SettingsWriterSinkConfiguration settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json")
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (string.IsNullOrEmpty(containerName)) containerName = "AppSettings";
			if (string.IsNullOrEmpty(fileName)) fileName = "SEttings.json";

			return settingsConfig.Sink(
				new AzureBlobStorageSink(
					connectionString,
					containerName,
					fileName
				));
		}
	}
}
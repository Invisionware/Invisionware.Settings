using System;
using Invisionware.Settings.Sinks.Azure;

namespace Invisionware.Settings.Sinks
{
	public static class AzureFileStorageSettingsConfigurationExtensions
	{
		/// <summary>
		/// Creates the Azures File Storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureFileStorage:ConnectionString
		///		settings:sink:AzureFileStorage:ContainerName
		///		settings:sink:AzureFileStorage:FileName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration AzureFileStorage(this SettingsReaderSinkConfiguration settingsConfig) 
		{
			return AzureFileStorage(settingsConfig,
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureFileStorage:ConnectionString"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureFileStorage:ContainerName"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureFileStorage:FileName"]
			);
		}

		public static SettingsConfiguration AzureFileStorage(this SettingsReaderSinkConfiguration settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json") 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (string.IsNullOrEmpty(containerName)) containerName = "AppSettings";
			if (string.IsNullOrEmpty(fileName)) fileName = "Settings.json";

			return settingsConfig.Sink(
				new AzureFileStorageSink(
					connectionString,
					containerName,
					fileName
					));
		}

		/// <summary>
		/// Creates the Azures File Storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureFileStorage:ConnectionString
		///		settings:sink:AzureFileStorage:ContainerName
		///		settings:sink:AzureFileStorage:FileName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration AzureFileStorage(this SettingsWriterSinkConfiguration settingsConfig) 
		{
			return AzureFileStorage(settingsConfig,
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureFileStorage:ConnectionString"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureFileStorage:ContainerName"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureFileStorage:FileName"]
			);
		}

		public static SettingsConfiguration AzureFileStorage(this SettingsWriterSinkConfiguration settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json") 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (string.IsNullOrEmpty(containerName)) containerName = "AppSettings";
			if (string.IsNullOrEmpty(fileName)) fileName = "Settings.json";

			return settingsConfig.Sink(
				new AzureFileStorageSink(
					connectionString,
					containerName,
					fileName
				));
		}
	}
}
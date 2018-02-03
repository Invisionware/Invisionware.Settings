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
		public static SettingsConfiguration<T> AzureFileStorage<T>(this SettingsReaderSinkConfiguration<T> settingsConfig, IAppConfigSettingsMgr appConfigSettingsMgr) where T : class, new()
		{
			if (appConfigSettingsMgr == null) throw new ArgumentNullException(nameof(appConfigSettingsMgr));

			return AzureFileStorage(settingsConfig,
				appConfigSettingsMgr.GetValue("settings:sink:AzureFileStorage:ConnectionString", string.Empty),
				appConfigSettingsMgr.GetValue("settings:sink:AzureFileStorage:ContainerName", "AppSettings"),
				appConfigSettingsMgr.GetValue("settings:sink:AzureFileStorage:FileName", "Settings.json")
			);
		}

		public static SettingsConfiguration<T> AzureFileStorage<T>(this SettingsReaderSinkConfiguration<T> settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json") where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new AzureFileStorageSink(
					connectionString,
					containerName,
					fileName
					));
		}

		public static SettingsConfiguration<T> AzureFileStorage<T>(this SettingsWriterSinkConfiguration<T> settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json") where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new AzureFileStorageSink(
					connectionString,
					containerName,
					fileName
				));
		}
	}
}
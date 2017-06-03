using System;
using Invisionware.Settings.Sinks.Azure;

namespace Invisionware.Settings.Sinks
{
	public static class AzureFileStorageSettingsConfigurationExtensions
	{
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
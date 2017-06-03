using System;
using Invisionware.Settings.Sinks.Azure;

namespace Invisionware.Settings.Sinks
{
	public static class AzureBlobSettingsConfigurationExtensions
	{
		public static SettingsConfiguration<T> AzureBlobStorage<T>(this SettingsReaderSinkConfiguration<T> settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json") where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new AzureBlobStorageSink(
					connectionString,
					containerName,
					fileName
					));
		}

		public static SettingsConfiguration<T> AzureBlobStorage<T>(this SettingsWriterSinkConfiguration<T> settingsConfig,
			string connectionString, string containerName = "AppSettings", string fileName = "Settings.json") where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new AzureBlobStorageSink(
					connectionString,
					containerName,
					fileName
				));
		}
	}
}
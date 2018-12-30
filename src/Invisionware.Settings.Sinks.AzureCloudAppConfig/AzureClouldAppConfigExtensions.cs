using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invisionware.Settings.Sinks.AzureCloudAppConfig
{
	public static class AzureClouldAppConfigExtensions
	{
		/// <summary>
		/// Initialzies the Azure Clould config Settings Reader Sink.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig</exception>
		public static SettingsConfiguration SystemAppConfig<T>(this SettingsReaderSinkConfiguration settingsConfig) where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new SettingsAzureClouldAppConfigSink());
		}
	}
}

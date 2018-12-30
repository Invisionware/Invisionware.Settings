using System;

namespace Invisionware.Settings.Sinks.SystemAppConfig
{
	public static class SystemAppConfigSinkExtensions
	{
		/// <summary>
		/// Initialzies the System app.config Settings Reader Sink.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig</exception>
		public static SettingsConfiguration SystemAppConfig(this SettingsReaderSinkConfiguration settingsConfig) 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new SettingsSystemAppConfigSink());
		}
	}
}

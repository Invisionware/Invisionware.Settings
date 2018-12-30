using System;
using System.Linq;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings.Sinks.SystemAppConfig
{
	public class SettingsSystemAppConfigSink : ISettingsValueReaderSink
	{
		public EventHandler<SettingsLoadingEventArgs> OnSettingsRead { get; set; }

		public bool Close()
		{
			return true;
		}

		public bool Flush()
		{
			return true;
		}

		public bool Open()
		{
			return true;
		}

		public T ReadSetting<T>(string key, T defaultValue = default(T))
		{
			if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(key))
			{
				var value = System.Configuration.ConfigurationManager.AppSettings[key];

				OnSettingsRead?.Invoke(this, new SettingsLoadingEventArgs { Data = value });

				var result = Convert.ChangeType(value, typeof(T));

				return (T) result;
			}

			return defaultValue;
		}
	}
}

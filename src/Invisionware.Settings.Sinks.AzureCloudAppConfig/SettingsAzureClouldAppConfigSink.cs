using System;
using Invisionware.Settings.EventArgs;
using Microsoft.Azure;

namespace Invisionware.Settings.Sinks.AzureCloudAppConfig
{
	class SettingsAzureClouldAppConfigSink : ISettingsValueReaderSink
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
			throw new NotImplementedException();
		}

		public T ReadSetting<T>(string key, T defaultValue = default(T))
		{
			try
			{
				var result = CloudConfigurationManager.GetSetting(key, false, true);

				OnSettingsRead?.Invoke(this, new SettingsLoadingEventArgs { Data = result });

				return (T)Convert.ChangeType(result, typeof(T));
			}
			catch
			{
				return defaultValue;
			}

		}
	}
}

using System;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings
{
	public interface ISettingsReaderSink : ISettingsSink
	{
		EventHandler<SettingsLoadingEventArgs> OnSettingsRead { get; set; }
	}
}
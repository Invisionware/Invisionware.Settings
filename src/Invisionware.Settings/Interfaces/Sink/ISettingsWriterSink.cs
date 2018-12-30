using System;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings
{
	public interface ISettingsWriterSink : ISettingsSink
	{
		EventHandler<SettingsSavingEventArgs> OnSettingsWritting { get; set; }
	}
}
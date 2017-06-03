using System;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings
{
	public interface ISettingsWriterSink : ISettingsSink
	{
		bool Save<T>(T settings) where T : class;

		EventHandler<SettingsSavingEventArgs> OnSettingsSaving { get; set; }
	}
}
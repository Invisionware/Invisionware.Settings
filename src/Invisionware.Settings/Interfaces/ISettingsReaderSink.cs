using System;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings
{
	public interface ISettingsReaderSink : ISettingsSink
	{
		T Load<T>() where T : class;

		EventHandler<SettingsLoadingEventArgs> OnSettingsLoading { get; set; }
	}
}
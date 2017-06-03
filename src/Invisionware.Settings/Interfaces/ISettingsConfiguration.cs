using System;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings
{
	public interface ISettingsConfiguration
	{
		/// <summary>
		/// Gets or sets the on settings loading event hander.
		/// </summary>
		/// <value>The on settings loading.</value>
		EventHandler<SettingsLoadingEventArgs> OnSettingsLoading { get; set; }

		/// <summary>
		/// Gets or sets the on settings saving event handler.
		/// </summary>
		/// <value>The on settings saving.</value>
		EventHandler<SettingsSavingEventArgs> OnSettingsSaving { get; set; }
	}
}
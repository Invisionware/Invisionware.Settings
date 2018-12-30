namespace Invisionware.Settings
{
	public sealed class AppConfigSettingsMgr : IAppConfigSettingsMgr
	{
		#region Static Methods
		public static IAppConfigSettingsMgr Current { get; private set; }

		/// <summary>
		/// Gets the value from the App Settings Provider.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static T GetValue<T>(string key, T defaultValue = default(T))
		{
			return Current.GetValue<T>(key, defaultValue);
		}

		/// <summary>
		/// Registers the specified configuration manager.
		/// </summary>
		/// <param name="configManager">The configuration manager.</param>
		/// <returns></returns>
		public static IAppConfigSettingsMgr Register(IAppConfigSettingsMgr configManager)
		{
			Current = configManager;

			Current.LoadSettings();

			return Current;
		}
		#endregion Static Methods

		#region Implementation of IAppConfigSettingsMgr
		/// <summary>
		/// Loads the settings.
		/// </summary>
		/// <returns></returns>
		public IAppConfigSettingsMgr LoadSettings()
		{
			return this;
		}

		/// <summary>
		/// Gets the value from the active config manager.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		T IAppConfigSettingsMgr.GetValue<T>(string key, T defaultValue)
		{
			return Current.GetValue<T>(key, defaultValue);
		}

		/// <summary>
		/// Gets the app settings with the specified key.
		/// </summary>
		/// <value>
		/// The <see cref="System.String"/>.
		/// </value>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public string this[string key] => Current.GetValue<string>(key);

		#endregion Implementation of IAppConfigSettingsMgr
	}
}

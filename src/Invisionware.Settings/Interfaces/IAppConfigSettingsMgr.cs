namespace Invisionware.Settings
{
	public interface IAppConfigSettingsMgr
	{
		/// <summary>
		/// Loads the settings.
		/// </summary>
		/// <returns></returns>
		IAppConfigSettingsMgr LoadSettings();

		/// <summary>
		/// Gets the setting for the specific key. If it is not found use the default value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		T GetValue<T>(string key, T defaultValue = default(T));

		/// <summary>
		/// Gets the setting associated with the specified key.
		/// </summary>
		/// <value>
		/// The <see cref="System.String"/>.
		/// </value>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		string this[string key] { get; }
	}
}
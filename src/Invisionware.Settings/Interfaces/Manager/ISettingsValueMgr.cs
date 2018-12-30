// ***********************************************************************
// Assembly         : Invisionware.Settings.
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="ISettingsMgr.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Invisionware.Settings
{
	/// <summary>
	/// Interface ISettingsMgr
	/// </summary>
	public interface ISettingsValueMgr : ISettingsMgr
	{
		/// <summary>
		/// Gets the setting for the specific key. If it is not found use the default value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		T ReadSetting<T>(string key, T defaultValue = default(T));

		/// <summary>
		/// Sets the value for the the specified setting key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		bool WriteSetting<T>(string key, T value);

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
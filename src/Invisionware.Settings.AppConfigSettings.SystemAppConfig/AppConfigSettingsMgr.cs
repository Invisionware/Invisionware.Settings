// ***********************************************************************
// Assembly         : Invisionware.Settings.AppConfigSettings.SystemAppConfig
// Author           :Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 12-28-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 12-28-2017
// ***********************************************************************
// <copyright file="AppConfigSettingsMgr.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Linq;

namespace Invisionware.Settings.AppConfigSettings.SystemAppConfig
{
	/// <summary>
	/// Class LocalAppConfigSettingsMgr.
	/// </summary>
	/// <seealso cref="Invisionware.Settings.IAppConfigSettingsMgr" />
	public class LocalAppConfigSettingsMgr : IAppConfigSettingsMgr
	{
		#region Implementation of IConfigSettingsMgr

		/// <summary>
		/// Loads the settings.
		/// </summary>
		/// <returns>IAppConfigSettingsMgr.</returns>
		public IAppConfigSettingsMgr LoadSettings()
		{
			return this;
		}

		/// <summary>
		/// Gets the setting for the specific key. If it is not found use the default value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>T.</returns>
		public T GetValue<T>(string key, T defaultValue = default(T))
		{
			if (!ConfigurationManager.AppSettings.AllKeys.Contains(key)) return defaultValue;

			var result = ConfigurationManager.AppSettings.Get(key);

			return (T) Convert.ChangeType(result, typeof(T));
		}

		/// <summary>
		/// Gets the <see cref="System.String"/> with the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>System.String.</returns>
		public string this[string key] => GetValue<string>(key, string.Empty);

		#endregion
	}
}

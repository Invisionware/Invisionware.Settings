// ***********************************************************************
// Assembly         : Invisionware.Settings.AppConfigSettings.CloudAppConfig
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 12-28-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 12-28-2017
// ***********************************************************************
// <copyright file="AzureConfigSettingsMgr.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Azure;

namespace Invisionware.Settings.AppConfigSettings.AzureCloudAppConfig
{
	/// <summary>
	/// Class AzureAppConfigSettingsMgr.
	/// </summary>
	/// <seealso cref="Invisionware.Settings.IAppConfigSettingsMgr" />
	public class AzureAppConfigSettingsMgr : IAppConfigSettingsMgr
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
			try
			{
				var result = CloudConfigurationManager.GetSetting(key, false, true);

				return (T) Convert.ChangeType(result, typeof(T));
			}
			catch
			{
				return defaultValue;
			}
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

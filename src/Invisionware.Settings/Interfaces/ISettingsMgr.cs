// ***********************************************************************
// Assembly         : Invisionware.Settings.Portable
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

using System;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings
{
	/// <summary>
	/// Interface ISettingsMgr
	/// </summary>
	public interface ISettingsMgr<T> where T : class, new()
	{
		/// <summary>
		/// Loads the settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		T LoadSettings();

		/// <summary>
		/// Loads the settings asynchronous.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Task&lt;T&gt;.</returns>
		Task<T> LoadSettingsAsync();

		/// <summary>
		/// Saves the settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		bool SaveSettings(T settings);

		/// <summary>
		/// Saves the settings asynchronous.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		Task<bool> SaveSettingsAsync(T settings);
	}
}
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

using System.Threading.Tasks;

namespace Invisionware.Settings
{
	/// <summary>
	/// Interface ISettingsMgr
	/// </summary>
	public interface ISettingsObjectMgrAsync : ISettingsMgr
	{
		/// <summary>
		/// Loads the settings asynchronous.
		/// </summary>
		/// <returns><c>true</c> if save was a success, <c>false</c> otherwise.</returns>
		Task<TSettingsClass> ReadSettingsAsync<TSettingsClass>() where TSettingsClass : class, new();

		/// <summary>
		/// Saves the settings asynchronous.
		/// </summary>
		/// <returns><c>true</c> if save was a success, <c>false</c> otherwise.</returns>
		Task<bool> WriteSettingsAsync<TSettingsClass>(TSettingsClass settingsObject) where TSettingsClass : class, new();
	}

}
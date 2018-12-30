// ***********************************************************************
// Assembly         : Invisionware.Settings.Portable
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsMgr.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Invisionware.Settings
{
	public static class SettingsManager<T> where T : ISettingsMgr
	{
		#region Static Properties
		public static T Settings { get; private set; }
		#endregion Static Properties

		#region Static Methods
		public static void SetSettingsManager(T settingsManager) { Settings = settingsManager; }
		#endregion Static Methods

	}
}
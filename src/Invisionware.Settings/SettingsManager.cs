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
	public static class SettingsManager
	{
		#region Static Properties
		private static ISettingsMgr _settingsManager { get; set; }
		#endregion Static Properties

		#region Static Methods
		public static void SetSettingsManager(ISettingsMgr settingsManager) { _settingsManager = settingsManager; }

		public static ISettingsObjectMgr AsObjectManager() { return _settingsManager as ISettingsObjectMgr; }
		public static ISettingsValueMgr AsValueManager() { return _settingsManager as ISettingsValueMgr; }

		public static T ReadSetting<T>(string key, T defaultValue = default(T))
		{
			if (!(_settingsManager is ISettingsValueMgr)) throw new System.InvalidCastException("SettingsManager does not support reading name/value pairs");

			return ((ISettingsValueMgr)_settingsManager).ReadSetting(key, defaultValue);
		}

		public static bool WriteSetting<T>(string key, T value)
		{
			if (!(_settingsManager is ISettingsValueMgr)) throw new System.InvalidCastException("SettingsManager does not support writting name/value pairs");

			return ((ISettingsValueMgr)_settingsManager).WriteSetting(key, value);
		}

		public static TSettingClass ReadSettings<TSettingClass>() where TSettingClass : class, new()
		{
			if (!(_settingsManager is ISettingsObjectMgr)) throw new System.InvalidCastException("SettingsManager does not support reading objects");

			return ((ISettingsObjectMgr)_settingsManager).ReadSettings<TSettingClass>();
		}

		public static bool WriteSettings<TSettingClass>(TSettingClass value) where TSettingClass : class, new()
		{
			if (!(_settingsManager is ISettingsObjectMgr)) throw new System.InvalidCastException("SettingsManager does not support writting objects");

			return ((ISettingsObjectMgr)_settingsManager).WriteSettings(value);
		}
		#endregion Static Methods

	}
}
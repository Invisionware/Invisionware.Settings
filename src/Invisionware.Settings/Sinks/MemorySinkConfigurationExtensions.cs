// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-10-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-10-2017
// ***********************************************************************
// <copyright file="MemorySinkConfigurationExtensions.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Invisionware.Settings.Sinks
{
	/// <summary>
	/// Class AzureConfigurationManagerSettingsConfigurationExtensions.
	/// </summary>
	public static class AzureConfigurationManagerSettingsConfigurationExtensions
	{
		/// <summary>
		/// Memories the specified settings value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <param name="settingsValue">The settings value.</param>
		/// <returns>SettingsConfiguration&lt;T&gt;.</returns>
		public static SettingsConfiguration Memory<T>(this SettingsReaderSinkConfiguration settings, T settingsValue)
		{
			return settings.Sink(new MemorySink(settingsValue));
		}

		/// <summary>
		/// Memories the specified settings value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <param name="settingsValue">The settings value.</param>
		/// <returns>SettingsConfiguration&lt;T&gt;.</returns>
		public static SettingsConfiguration Memory<T>(this SettingsWriterSinkConfiguration settings, T settingsValue)
		{
			return settings.Sink(new MemorySink(settingsValue));
		}
	}
}
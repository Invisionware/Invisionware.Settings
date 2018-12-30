// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.JsonNet
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="JsonNetSettingsConfigurationExtensions.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Invisionware.Settings.Sinks
{
	/// <summary>
	/// Class AzureDocumentDbSettingsConfigurationExtensions.
	/// </summary>
	public static class JsonNetSettingsConfigurationExtensions
	{
		/// <summary>
		/// Creates the JsonNet Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:JsonNet:FileName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration JsonNet(this SettingsReaderSinkConfiguration settingsConfig) 
		{
			return JsonNet(settingsConfig,
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:JsonNet:FileName"]
			);
		}

		/// <summary>
		/// Initialzies the Json.NET Settings Reader Sing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="jsonSettings">The json settings.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig</exception>
		public static SettingsConfiguration JsonNet(this SettingsReaderSinkConfiguration settingsConfig, string fileName, Newtonsoft.Json.JsonSerializerSettings jsonSettings = null) 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));

			return settingsConfig.Sink(
				new SettingsJsonSink(fileName, jsonSettings));
		}

		/// <summary>
		/// Creates the JsonNet Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:JsonNet:FileName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration JsonNet(this SettingsWriterSinkConfiguration settingsConfig)
		{
			return JsonNet(settingsConfig,
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:JsonNet:FileName"]
			);
		}

		/// <summary>
		/// Initialzies the Json.NET Settings Writer Sing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="jsonSettings">The json settings.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig</exception>
		public static SettingsConfiguration JsonNet(this SettingsWriterSinkConfiguration settingsConfig, string fileName, Newtonsoft.Json.JsonSerializerSettings jsonSettings = null) 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new SettingsJsonSink(fileName, jsonSettings));
		}
	}
}
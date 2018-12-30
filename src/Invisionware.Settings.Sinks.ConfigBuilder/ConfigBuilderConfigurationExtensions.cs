// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.ConfigBuilder
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="ConfigBuilderSettingsConfigurationExtensions.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Invisionware.Settings.Sinks.ConfigBuilder;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Invisionware.Settings.Sinks
{
	/// <summary>
	/// Class AzureDocumentDbSettingsConfigurationExtensions.
	/// </summary>
	public static class ConfigBuilderSettingsConfigurationExtensions
	{
		/// <summary>
		/// Creates the ConfigBuilder Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:ConfigBuilder:FileName
		/// </summary>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration ConfigBuilder(this SettingsReaderSinkConfiguration settingsConfig) 
		{
			if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("settings:sink:ConfigBuilder:FileName"))
			{
				return ConfigBuilder(settingsConfig,
					System.Configuration.ConfigurationManager.AppSettings["settings:sink:ConfigBuilder:FileName"]
				);
			}

			return settingsConfig.Sink(new ConfigBuilderSink());
		}

		/// <summary>
		/// Creates the ConfigBuilder Configuiration object and loads the main settings form the config manager.
		/// </summary>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="configurationBuilder">the configuration builder instance to use</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration ConfigBuilder(this SettingsReaderSinkConfiguration settingsConfig, IConfigurationBuilder configurationBuilder)
		{
			return settingsConfig.Sink(new ConfigBuilderSink(configurationBuilder));
		}

		/// <summary>
		/// Creates the ConfigBuilder Configuiration object and loads the main settings form the config manager.
		/// </summary>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="configurationFile">Name of the file.</param>
		/// <param name="optional">The json settings.</param>
		/// <param name="fileType">Indicates the file type being used (json or xml)</param>
		/// <param name="reloadOnChange">Indicates if the settings should automatically reload when a file change is detected</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig</exception>
		public static SettingsConfiguration ConfigBuilder(this SettingsReaderSinkConfiguration settingsConfig, string configurationFile, bool optional = false, bool reloadOnChange = false, ConfigurationFileTypes fileType = ConfigurationFileTypes.Json) 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (string.IsNullOrEmpty(configurationFile)) throw new ArgumentNullException(nameof(configurationFile));

			return settingsConfig.Sink(
				new ConfigBuilderSink(configurationFile, optional, reloadOnChange, fileType));
		}
	}
}
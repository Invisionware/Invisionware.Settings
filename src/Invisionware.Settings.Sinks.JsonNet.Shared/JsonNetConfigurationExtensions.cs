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
		/// Initialzies the Json.NET Settings Writer Sing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="jsonSettings">The json settings.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig</exception>
		public static SettingsConfiguration<T> JsonNet<T>(this SettingsWriterSinkConfiguration<T> settingsConfig, string fileName, Newtonsoft.Json.JsonSerializerSettings jsonSettings = null) where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new SettingsJsonSink(fileName, jsonSettings));
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
		public static SettingsConfiguration<T> JsonNet<T>(this SettingsReaderSinkConfiguration<T> settingsConfig, string fileName, Newtonsoft.Json.JsonSerializerSettings jsonSettings = null) where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));

			return settingsConfig.Sink(
				new SettingsJsonSink(fileName, jsonSettings));
		}
	}
}
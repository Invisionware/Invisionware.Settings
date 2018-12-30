// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.Azure
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="AzureDocumentDbSettingsConfigurationExtensions.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Invisionware.Settings.Sinks.Azure;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace Invisionware.Settings.Sinks
{
	/// <summary>
	/// Class AzureDocumentDbSettingsConfigurationExtensions.
	/// </summary>
	public static class AzureDocumentDbSettingsConfigurationExtensions
	{
		/// <summary>
		/// Creates the Azures the file storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureFileStorage:EndPointUri
		///		settings:sink:AzureFileStorage:AuthorizationKey
		///		settings:sink:AzureFileStorage:DatabaseName
		///		settings:sink:AzureFileStorage:CollectionName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration AzureDocumentDb(this SettingsReaderSinkConfiguration settingsConfig) 
		{
			return AzureDocumentDb(settingsConfig,
				new Uri(System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPointUri"]),
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthorizationKey"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:DatabaseName"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:CollectionName"]
			);
		}

		/// <summary>
		/// Azures the document database.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="endpointUri">The endpoint URI.</param>
		/// <param name="authorizationKey">The authorization key.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="collectionName">Name of the collection.</param>
		/// <param name="connectionProtocol">The connection protocol.</param>
		/// <param name="jsonSettings">The json settings.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig
		/// or
		/// endpointUri
		/// or
		/// authorizationKey</exception>
		public static SettingsConfiguration AzureDocumentDb(this SettingsReaderSinkConfiguration settingsConfig, Uri endpointUri,
			string authorizationKey, string databaseName = "AppSettings", string collectionName = "Config", Protocol connectionProtocol = Protocol.Https, JsonSerializerSettings jsonSettings = null) 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (endpointUri == null) throw new ArgumentNullException(nameof(endpointUri));
			if (authorizationKey == null) throw new ArgumentNullException(nameof(authorizationKey));
			if (string.IsNullOrEmpty(databaseName)) databaseName = "AppSettings";
			if (string.IsNullOrEmpty(collectionName)) collectionName = "Config";

			return settingsConfig.Sink(
				new AzureDocumentDbSink(
					endpointUri,
					authorizationKey,
					databaseName,
					collectionName,
					connectionProtocol: connectionProtocol, 
					jsonSettings: jsonSettings));
		}

		/// <summary>
		/// Creates the Azures the file storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureFileStorage:EndPointUri
		///		settings:sink:AzureFileStorage:AuthorizationKey
		///		settings:sink:AzureFileStorage:DatabaseName
		///		settings:sink:AzureFileStorage:CollectionName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration AzureDocumentDb(this SettingsWriterSinkConfiguration settingsConfig) 
		{
			return AzureDocumentDb(settingsConfig,
				new Uri(System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPointUri"]),
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthorizationKey"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:DatabaseName"],
				System.Configuration.ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:CollectionName"]
			);
		}

		/// <summary>
		/// Azures the document database.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="endpointUri">The endpoint URI.</param>
		/// <param name="authorizationKey">The authorization key.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="collectionName">Name of the collection.</param>
		/// <param name="maxVersions">The maximum versions.</param>
		/// <param name="connectionProtocol">The connection protocol.</param>
		/// <param name="jsonSettings">The json settings.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig
		/// or
		/// endpointUri
		/// or
		/// authorizationKey</exception>
		public static SettingsConfiguration AzureDocumentDb(this SettingsWriterSinkConfiguration settingsConfig, Uri endpointUri,
			string authorizationKey, string databaseName = "AppSettings", string collectionName = "Config", int maxVersions = 3, Protocol connectionProtocol = Protocol.Https, JsonSerializerSettings jsonSettings = null) 
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (endpointUri == null) throw new ArgumentNullException(nameof(endpointUri));
			if (authorizationKey == null) throw new ArgumentNullException(nameof(authorizationKey));
			if (string.IsNullOrEmpty(databaseName)) databaseName = "AppSettings";
			if (string.IsNullOrEmpty(collectionName)) collectionName = "Config";

			return settingsConfig.Sink(
				new AzureDocumentDbSink(
					endpointUri,
					authorizationKey,
					databaseName,
					collectionName,
					maxVersions,
					connectionProtocol,
					jsonSettings));
		}
	}
}
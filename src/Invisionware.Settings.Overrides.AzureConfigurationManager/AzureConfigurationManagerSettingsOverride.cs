// ***********************************************************************
// Assembly         : Invisionware.Settings.Override.AzureConfigurationManager
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="AzureConfigurationManagerSettingsOverride.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Azure;

namespace Invisionware.Settings.Overrides.Azure
{
	/// <summary>
	/// Class AzureConfigurationManagerSettingsOverride.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="ISettingsOverride{T}" />
	public class AzureConfigurationManagerSettingsOverride<T> : ISettingsOverride<T>
	{
		public AzureConfigurationManagerSettingsOverride(IDictionary<string, Action<T, string>> mappings = null)
		{
			if (mappings == null) mappings = new ConcurrentDictionary<string, Action<T, string>>();

			Mappings = mappings;
		}

		public IDictionary<string, Action<T, string>> Mappings { get; set; }

		public AzureConfigurationManagerSettingsOverride<T> AddMapping(string appSettingsKeyName, Action<T, string> mapAction)
		{
			Mappings[appSettingsKeyName] = mapAction;

			return this;
		}

		#region Implementation of ISettingsOverride<T>

		/// <summary>
		/// Enriches the specified settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Enrich(T settings)
		{
			foreach (var kvp in Mappings)
			{
				kvp.Value(settings, CloudConfigurationManager.GetSetting(kvp.Key, false, false));
			}
		}

		#endregion
	}
}
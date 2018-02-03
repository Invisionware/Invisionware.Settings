// ***********************************************************************
// Assembly         : Invisionware.Settings.Override.AppConfig
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-10-2017
// ***********************************************************************
// <copyright file="AppConfigSettingsOverride.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;

namespace Invisionware.Settings.Overrides.AppConfig
{
	/// <summary>
	/// Class AppConfigOverride.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="ISettingsOverride{T}" />
	public class AppConfigOverride<T> : ISettingsOverride<T>
	{
		public AppConfigOverride(IDictionary<string, Action<T, string>> mappings = null)
		{
			if (mappings == null) mappings = new ConcurrentDictionary<string, Action<T, string>>();

			Mappings = mappings;
		}

		public IDictionary<string, Action<T, string>> Mappings { get; set; }

		public AppConfigOverride<T> AddMapping(string appSettingsKeyName, Action<T, string> mapAction)
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
				kvp.Value(settings, ConfigurationManager.AppSettings[kvp.Key]);
			}
		}

		#endregion
	}
}
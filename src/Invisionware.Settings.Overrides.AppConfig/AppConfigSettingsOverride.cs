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
	public class AppConfigOverride : ISettingsValueOverride
	{
		public AppConfigOverride(IDictionary<string, Func<string, object, object>> mappings = null)
		{
			if (mappings == null) mappings = new ConcurrentDictionary<string, Func<string, object, object>>();

			Mappings = mappings;
		}

		public IDictionary<string, Func<string, object, object>> Mappings { get; set; }

		public AppConfigOverride AddMapping(string appSettingsKeyName, Func<string, object, object> mapAction)
		{
			Mappings[appSettingsKeyName] = mapAction;

			return this;
		}

		#region Implementation of ISettingsOverride<T>

		/// <summary>
		/// Enriches the specified settings.
		/// </summary>
		/// <param name="value">The settings.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public T Enrich<T>(string key, T value)
		{
			if (Mappings.Keys.Contains(key))
			{
				value = (T)Mappings[key](key, value);
			}

			return value;
		}

		#endregion
	}
}
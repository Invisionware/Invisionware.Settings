// ***********************************************************************
// Assembly         : Invisionware.Settings.Override.AppConfig
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-10-2017
// ***********************************************************************
// <copyright file="AppConfigSettingsOverrideExtensions.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Invisionware.Settings.Overrides.AppConfig;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class AppConfigSettingsOverrideExtensions.
	/// </summary>
	public static class AppConfigSettingsOverrideExtensions
	{
		/// <summary>
		/// Withhes the application configuration.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="overrideConfiguration">The enrichment configuration.</param>
		/// <param name="mappings">The mappings.</param>
		/// <returns>
		/// SettingsConfiguration&lt;T&gt;.
		/// </returns>
		/// <exception cref="ArgumentNullException">overrideConfiguration</exception>
		/// <exception cref="System.ArgumentNullException">overrideConfiguration</exception>
		public static SettingsConfiguration WithhAppConfig(this SettingsOverrideConfiguration overrideConfiguration, IDictionary<string, Func<string, object, object>> mappings) 
		{
			if (overrideConfiguration == null) throw new ArgumentNullException(nameof(overrideConfiguration));

			return overrideConfiguration.With(new AppConfigOverride(mappings));
		}
	}
}
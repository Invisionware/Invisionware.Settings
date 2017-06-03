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
		/// <returns>SettingsConfiguration&lt;T&gt;.</returns>
		/// <exception cref="System.ArgumentNullException">overrideConfiguration</exception>
		public static SettingsConfiguration<T> WithhAppConfig<T>(this SettingsOverrideConfiguration<T> overrideConfiguration) where T : class, new()
		{
			if (overrideConfiguration == null) throw new ArgumentNullException(nameof(overrideConfiguration));

			//return overrideConfiguration.With(new AzureConfigurationManagerSettingsOverride<T>());
			return null;
		}
	}
}
// ***********************************************************************
// Assembly         : Invisionware.Settings.Override.AzureConfigurationManager
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-10-2017
// ***********************************************************************
// <copyright file="AzureConfigurationManagerSettingsOverrideExtensions.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Invisionware.Settings.Overrides.Azure;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class AzureConfigurationManagerSettingsOverrideExtensions.
	/// </summary>
	public static class AzureConfigurationManagerSettingsOverrideExtensions
	{
		/// <summary>
		/// Azures the configuration manager.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="overrideConfiguration">The override configuration.</param>
		/// <returns>SettingsConfiguration&lt;T&gt;.</returns>
		/// <exception cref="System.ArgumentNullException">overrideConfiguration</exception>
		public static SettingsConfiguration<T> WithAzureConfigurationManager<T>(this SettingsOverrideConfiguration<T> overrideConfiguration) where T : class, new()
		{
			if (overrideConfiguration == null) throw new ArgumentNullException(nameof(overrideConfiguration));

			return overrideConfiguration.With(new AzureConfigurationManagerSettingsOverride<T>());
		}
	}
}
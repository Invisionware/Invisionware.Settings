// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-10-2017
// ***********************************************************************
// <copyright file="SettingsActionOverrideEnricherExtensions.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Invisionware.Settings.Overrides;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsOverrideEnricherExtensions.
	/// </summary>
	public static class SettingsValueOverrideExtensions
	{
		/// <summary>
		/// Withes the action.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="overrideConfiguration">The enrichment configuration.</param>
		/// <param name="action">The action.</param>
		/// <returns>SettingsConfiguration&lt;T&gt;.</returns>
		/// <exception cref="System.ArgumentNullException">overrideConfiguration</exception>
		public static SettingsConfiguration WithAction<T>(
			this SettingsOverrideConfiguration overrideConfiguration, Func<string, T, T> enrichFunc) where T : class, new()
		{
			if (overrideConfiguration == null) throw new ArgumentNullException(nameof(overrideConfiguration));
			return overrideConfiguration.With(new SettingsValueOverrideAction<T>(enrichFunc));
		}
	}
}
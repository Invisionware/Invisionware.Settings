// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsOverrideConfiguration.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsOverrideConfiguration.
	/// </summary>
	public class SettingsOverrideConfiguration
	{
		/// <summary>
		/// The settings configuration
		/// </summary>
		readonly SettingsConfiguration _settingsConfiguration;
		/// <summary>
		/// The add enricher
		/// </summary>
		readonly Action<ISettingsOverride> _addOverride;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsOverrideConfiguration"/> class.
		/// </summary>
		/// <param name="settingsConfiguration">The settings configuration.</param>
		/// <param name="addOverride">The add enricher.</param>
		/// <exception cref="System.ArgumentNullException">
		/// settingsConfiguration
		/// or
		/// addOverride
		/// </exception>
		internal SettingsOverrideConfiguration(
			SettingsConfiguration settingsConfiguration,
			Action<ISettingsOverride> addOverride)
		{
			_settingsConfiguration = settingsConfiguration ?? throw new ArgumentNullException(nameof(settingsConfiguration));
			_addOverride = addOverride ?? throw new ArgumentNullException(nameof(addOverride));
		}

		/// <summary>
		/// Withes the specified overrideEnrichers.
		/// </summary>
		/// <param name="overrideEnrichers">The overrideEnrichers.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">overrideEnrichers</exception>
		/// <exception cref="System.ArgumentException">Null enricher is not allowed.</exception>
		public SettingsConfiguration With(params ISettingsOverride[] overrideEnrichers)
		{
			if (overrideEnrichers == null) throw new ArgumentNullException(nameof(overrideEnrichers));

			foreach (var e in overrideEnrichers)
			{
				if (e == null)
				{
					throw new ArgumentException("Null override is not allowed.");
				}

				_addOverride(e);
			}
			return _settingsConfiguration;
		}
	}
}
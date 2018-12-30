// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsOverrideAction.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Invisionware.Settings.Overrides
{
	/// <summary>
	/// Class SettingsOverrideAction.
	/// </summary>
	public class SettingsOverrideAction<TSettingsClass> : ISettingsObjectOverride<TSettingsClass>
	{
		/// <summary>
		/// The action
		/// </summary>
		private readonly Func<TSettingsClass, TSettingsClass> _enrichFunc;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsOverrideAction{TSettingsClass}"/> class.
		/// </summary>
		/// <param name="enrichFunc">The action.</param>
		public SettingsOverrideAction(Func<TSettingsClass, TSettingsClass> enrichFunc)
		{
			_enrichFunc = enrichFunc;
		}

		#region Implementation of ISettingsObjectOverride

		/// <summary>
		/// Enriches the specified settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		public TSettingsClass Enrich(TSettingsClass settings)
		{
			var value = _enrichFunc(settings);

			return value;
		}
		#endregion Implementation of ISettingsObjectOverride
	}
}
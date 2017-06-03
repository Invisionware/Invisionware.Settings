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
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="ISettingsOverride{T}" />
	public class SettingsOverrideAction<T> : ISettingsOverride<T>
	{
		/// <summary>
		/// The action
		/// </summary>
		private readonly Action<T> _action;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsOverrideAction{T}"/> class.
		/// </summary>
		/// <param name="action">The action.</param>
		public SettingsOverrideAction(Action<T> action)
		{
			_action = action;
		}

		#region Implementation of ISettingsOverride

		/// <summary>
		/// Enriches the specified settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		public void Enrich(T settings)
		{
			_action(settings);
		}

		#endregion
	}
}
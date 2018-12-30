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
	/// Class SettingsValueOverrideAction.
	/// </summary>
	public class SettingsValueOverrideAction<T> : ISettingsValueOverride where T : class
	{
		/// <summary>
		/// The action
		/// </summary>
		private readonly Func<string, T, T> _enrichFunc;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsValueOverrideAction"/> class.
		/// </summary>
		/// <param name="enrichFunc">The action.</param>
		public SettingsValueOverrideAction(Func<string, T, T> enrichFunc)
		{
			_enrichFunc = enrichFunc;
		}

		#region Implementation of ISettingsValueOverride

		public TValue Enrich<TValue>(string key, TValue value) 
		{
			var tmp = value as T;

			var result = (TValue) Convert.ChangeType(_enrichFunc(key, tmp), typeof(TValue));

			return result;
		}
		#endregion
	}
}
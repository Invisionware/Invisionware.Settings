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
namespace Invisionware.Settings.Override.AppConfig
{
	/// <summary>
	/// Class AppConfigOverride.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="ISettingsOverride{T}" />
	public class AppConfigOverride<T> : ISettingsOverride<T>
	{
		#region Implementation of ISettingsOverride<T>

		/// <summary>
		/// Enriches the specified settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Enrich(T settings)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
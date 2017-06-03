// ***********************************************************************
// Assembly         : Invisionware.Settings.Override.AzureConfigurationManager
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="AzureConfigurationManagerSettingsOverride.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Invisionware.Settings.Override.Azure
{
	/// <summary>
	/// Class AzureConfigurationManagerSettingsOverride.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="ISettingsOverride{T}" />
	public class AzureConfigurationManagerSettingsOverride<T> : ISettingsOverride<T>
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
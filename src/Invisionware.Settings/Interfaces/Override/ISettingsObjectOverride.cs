// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="ISettingsOverride.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Invisionware.Settings
{
	public interface ISettingsObjectOverride<TSettingsClass> : ISettingsOverride
	{
		/// <summary>
		/// Enriches the specified settings objet.
		/// </summary>
		/// <param name="settings">The settings.</param>
		TSettingsClass Enrich(TSettingsClass settings);
	}
}
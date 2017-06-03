// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsReaderSinkConfiguration.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Invisionware.Settings.Sinks;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsReaderSinkConfiguration.
	/// </summary>
	/// <typeparam name="TSetting">The type of the t setting.</typeparam>
	/// <seealso cref="Invisionware.Settings.SettingsSinkConfiguration{TSetting, Invisionware.Settings.Sinks.ISettingsReaderSink}" />
	public class SettingsReaderSinkConfiguration<TSetting> : SettingsSinkConfiguration<TSetting, ISettingsReaderSink> where TSetting : class, new()
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsReaderSinkConfiguration{TSetting}"/> class.
		/// </summary>
		/// <param name="settingsConfiguration">The settings configuration.</param>
		/// <param name="addSink">The add sink.</param>
		internal SettingsReaderSinkConfiguration(SettingsConfiguration<TSetting> settingsConfiguration, Action<ISettingsReaderSink> addSink) : base(
			settingsConfiguration, addSink)
		{
			
		}
	}
}
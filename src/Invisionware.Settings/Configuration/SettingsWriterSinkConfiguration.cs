// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsWriterSinkConfiguration.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Invisionware.Settings.Sinks;

namespace Invisionware.Settings
{
	public class SettingsWriterSinkConfiguration : SettingsSinkConfiguration<ISettingsWriterSink>
	{
		internal SettingsWriterSinkConfiguration(SettingsConfiguration settingsConfiguration, Action<ISettingsWriterSink> addSink) : base(
			settingsConfiguration, addSink)
		{

		}
	}
}
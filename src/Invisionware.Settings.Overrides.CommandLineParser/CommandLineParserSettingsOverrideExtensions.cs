// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.CommandLineParser
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="CommandLineParserSettingsOverrideExtensions.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Invisionware.Settings.Overrides.CommandLineParser;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class CommandLineSettingsConfigurationExtensions.
	/// </summary>
	public static class CommandLineParserSettingsOverrideExtensions
	{
		/// <summary>
		/// Commands the line.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="overrideConfiguration">The enrichment configuration.</param>
		/// <param name="commandLine">The command line.</param>
		/// <param name="args">The arguments.</param>
		/// <returns>SettingsConfiguration.</returns>
		public static SettingsConfiguration<T> WithCommandLineParser<T>(this SettingsOverrideConfiguration<T> overrideConfiguration, string commandLine, string[] args) where T : class, new()
		{
			if (overrideConfiguration == null) throw new ArgumentNullException(nameof(overrideConfiguration));

			return overrideConfiguration.With(new CommandLineParserSettingsOverride<T>(commandLine, args));
		}
	}
}
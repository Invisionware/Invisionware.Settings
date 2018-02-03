// ***********************************************************************
// Assembly         : Invisionware.Settings.Override.CommandLine
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-10-2017
// ***********************************************************************
// <copyright file="CommandLineParserSettingsOverride.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Invisionware.Settings.Overrides.CommandLineParser
{
	/// <summary>
	/// Class CommandLineParserSettingsOverride.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="ISettingsOverride{T}" />
	public class CommandLineParserSettingsOverride<T> : ISettingsOverride<T>
	{
		/// <summary>
		/// The command line
		/// </summary>
		private string _commandLine;
		/// <summary>
		/// The arguments
		/// </summary>
		private string[] _args;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandLineParserSettingsOverride{T}"/> class.
		/// </summary>
		/// <param name="commandLine">The command line.</param>
		/// <param name="args">The arguments.</param>
		public CommandLineParserSettingsOverride(string commandLine, string[] args)
		{
			_commandLine = commandLine;
			_args = args;
		}

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
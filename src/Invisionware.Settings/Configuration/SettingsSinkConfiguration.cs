// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsSinkConfiguration.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Invisionware.Settings.Sinks;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsSinkConfiguration.
	/// </summary>
	/// <typeparam name="TSetting">The type of the t setting.</typeparam>
	/// <typeparam name="TSink">The type of the t sink.</typeparam>
	public class SettingsSinkConfiguration<TSetting, TSink> where TSetting : class, new()
	{
		/// <summary>
		/// The settings configuration
		/// </summary>
		private readonly SettingsConfiguration<TSetting> _settingsConfiguration;
		/// <summary>
		/// The add sink
		/// </summary>
		private readonly Action<TSink> _addSink;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsSinkConfiguration{T}"/> class.
		/// </summary>
		/// <param name="settingsConfiguration">The settings configuration.</param>
		/// <param name="addSink">The add sink.</param>
		/// <exception cref="System.ArgumentNullException">
		/// settingsConfiguration
		/// or
		/// addSink
		/// </exception>
		internal SettingsSinkConfiguration(SettingsConfiguration<TSetting> settingsConfiguration, Action<TSink> addSink)
		{
			if (settingsConfiguration == null) throw new ArgumentNullException(nameof(settingsConfiguration));
			if (addSink == null) throw new ArgumentNullException(nameof(addSink));

			_settingsConfiguration = settingsConfiguration;
			_addSink = addSink;
		}

		/// <summary>
		/// Sinks the specified settings sink.
		/// </summary>
		/// <param name="settingsSink">The settings sink.</param>
		/// <returns>SettingsConfiguration.</returns>
		public SettingsConfiguration<TSetting> Sink(
			TSink settingsSink)
		{
			var sink = settingsSink;

			_addSink(sink);

			return _settingsConfiguration;
		}

		public static SettingsConfiguration<TSetting> Wrap(
			SettingsReaderSinkConfiguration<TSetting> settingsSinkConfiguration,
			Func<ISettingsReaderSink, ISettingsReaderSink> wrapSink,
			Action<SettingsReaderSinkConfiguration<TSetting>> configureWrappedSink)
		{
			if (settingsSinkConfiguration == null) throw new ArgumentNullException(nameof(settingsSinkConfiguration));
			if (wrapSink == null) throw new ArgumentNullException(nameof(wrapSink));
			if (configureWrappedSink == null) throw new ArgumentNullException(nameof(configureWrappedSink));

			void WrapAndAddSink(ISettingsReaderSink sink)
			{
				bool sinkIsDisposable = sink is IDisposable;

				var wrappedSink = wrapSink(sink);

				if (sinkIsDisposable && !(wrappedSink is IDisposable))
				{
					//SelfLog.WriteLine("Wrapping sink {0} does not implement IDisposable, but wrapped sink {1} does.", wrappedSink, sink);
				}

				settingsSinkConfiguration.Sink(wrappedSink);
			}

			var capturingSettingsSinkConfiguration = new SettingsReaderSinkConfiguration<TSetting>(
				settingsSinkConfiguration._settingsConfiguration,
				WrapAndAddSink);

			configureWrappedSink(capturingSettingsSinkConfiguration);

			return settingsSinkConfiguration._settingsConfiguration;
		}
	}
}
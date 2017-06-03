// ***********************************************************************
// Assembly         : Invisionware.Settings.Portable
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsConfiguration.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Invisionware.Settings.Sinks;
using System;
using System.Collections.Generic;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsConfiguration.
	/// </summary>
	/// <seealso cref="Invisionware.Settings.ISettingsConfiguration" />
	public class SettingsConfiguration<T> : ISettingsConfiguration where T : class, new()
	{
		#region Member Variables
		/// <summary>
		/// The settings MGR created
		/// </summary>
		private bool _settingsMgrCreated;

		/// <summary>
		/// The settings writer sink
		/// </summary>
		private ISettingsWriterSink _settingsWriterSink;
		/// <summary>
		/// The settings reader sink
		/// </summary>
		private ISettingsReaderSink _settingsReaderSink;

		/// <summary>
		/// The override readers
		/// </summary>
		private readonly List<ISettingsOverride<T>> _overrideReaders = new List<ISettingsOverride<T>>();

		/// <summary>
		/// The overrides writer
		/// </summary>
		private readonly List<ISettingsOverride<T>> _overridesWriter = new List<ISettingsOverride<T>>();
		#endregion Member Variables

		#region Public Properties
		/// <summary>
		/// Gets the write to.
		/// </summary>
		/// <value>The write to.</value>
		public SettingsWriterSinkConfiguration<T> WriteTo => new SettingsWriterSinkConfiguration<T>(this, s => _settingsWriterSink = s);
		/// <summary>
		/// Gets the read from.
		/// </summary>
		/// <value>The read from.</value>
		public SettingsReaderSinkConfiguration<T> ReadFrom => new SettingsReaderSinkConfiguration<T>(this, s => _settingsReaderSink = s);

		/// <summary>
		/// Gets the enrich.
		/// </summary>
		/// <value>The enrich.</value>
		public SettingsOverrideConfiguration<T> ReaderOverrides => new SettingsOverrideConfiguration<T>(this, e => _overrideReaders.Add(e));

		/// <summary>
		/// Gets the enrich.
		/// </summary>
		/// <value>The enrich.</value>
		public SettingsOverrideConfiguration<T> WriterOverrides => new SettingsOverrideConfiguration<T>(this, e => _overridesWriter.Add(e));
		#endregion Public Properties

		#region Event Handlers
		/// <summary>
		/// Gets or sets the on settings loading event hander.
		/// </summary>
		/// <value>The on settings loading.</value>
		public EventHandler<SettingsLoadingEventArgs> OnSettingsLoading { get; set; }
		/// <summary>
		/// Gets or sets the on settings saving event handler.
		/// </summary>
		/// <value>The on settings saving.</value>
		public EventHandler<SettingsSavingEventArgs> OnSettingsSaving { get; set; }
		#endregion Event Handlers


		/// <summary>
		/// Creates the settings MGR.
		/// </summary>
		/// <returns>ISettingsMgr.</returns>
		/// <exception cref="System.InvalidOperationException">CreateSettingsMgr() was previously called and can only be called once.</exception>
		public ISettingsMgr<T> CreateSettingsMgr()
		{
			if (_settingsMgrCreated)
				throw new InvalidOperationException("CreateSettingsMgr() was previously called and can only be called once.");

			_settingsMgrCreated = true;

			if (OnSettingsLoading != null)
			{
				_settingsReaderSink.OnSettingsLoading += OnSettingsLoading;
			}

			if (OnSettingsSaving != null)
			{
				_settingsWriterSink.OnSettingsSaving += OnSettingsSaving;
			}

			return new SettingsMgr<T>(_settingsReaderSink, _settingsWriterSink, _overrideReaders, _overridesWriter);
		}

	}
}
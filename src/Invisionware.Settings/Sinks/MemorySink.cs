// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-10-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-10-2017
// ***********************************************************************
// <copyright file="MemorySink.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;
using Invisionware.Settings.Sinks;

namespace Invisionware.Settings.Sinks
{
	/// <summary>
	/// Class MemorySink.
	/// </summary>
	/// <seealso cref="ISettingsReaderSink" />
	/// <seealso cref="ISettingsWriterSink" />
	public class MemorySink : ISettingsReaderSink, ISettingsWriterSink
	{
		#region Member Variables
		/// <summary>
		/// The current value
		/// </summary>
		private dynamic _currentValue;
		#endregion Member Variables

		/// <summary>
		/// Initializes a new instance of the <see cref="MemorySink"/> class.
		/// </summary>
		/// <param name="settingsValue">The settings value.</param>
		public MemorySink(dynamic settingsValue)
		{
			_currentValue = settingsValue;
		}

		#region Event Handlers
		public EventHandler<SettingsLoadingEventArgs> OnSettingsLoading { get; set; }
		public EventHandler<SettingsSavingEventArgs> OnSettingsSaving { get; set; }
		#endregion Event Handlers

		#region Implementation of ISettingsAsync
		public bool Open() { return true; }
		public bool Flush() { return true; }
		public bool Close() { return true; }
		#endregion Implementation of ISettingsAsync

		#region Implementation of ISettingsWriterSink
		/// <summary>
		/// Saves the specified settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool Save<T>(T settings) where T : class
		{
			OnSettingsSaving?.Invoke(this, new SettingsSavingEventArgs() { Data = settings });
			_currentValue = settings;

			return true;
		}
		#endregion

		#region Implementation of ISettingsReaderSink
		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T Load<T>() where T : class
		{
			OnSettingsLoading?.Invoke(this, new SettingsLoadingEventArgs() { Data = _currentValue });

			return _currentValue;
		}
		#endregion
	}
}
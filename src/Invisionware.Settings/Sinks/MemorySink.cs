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
	/// <seealso cref="ISettingsObjectReaderSink" />
	/// <seealso cref="ISettingsValueReaderSink" />
	/// <seealso cref="ISettingsObjectWriterSink" />
	/// <seealso cref="ISettingsValueWriterSink" />
	public class MemorySink : ISettingsObjectReaderSink, ISettingsValueReaderSink, ISettingsObjectWriterSink, ISettingsValueWriterSink
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
		public EventHandler<SettingsLoadingEventArgs> OnSettingsRead { get; set; }
		public EventHandler<SettingsSavingEventArgs> OnSettingsWritting { get; set; }
		#endregion Event Handlers

		#region Implementation of ISettingsAsync
		public bool Open() { return true; }
		public bool Flush() { return true; }
		public bool Close() { return true; }
		#endregion Implementation of ISettingsAsync

		#region Implementation of ISettingsObjectReaderSink
		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T ReadSetting<T>() where T : class
		{
			OnSettingsRead?.Invoke(this, new SettingsLoadingEventArgs() { Data = _currentValue });

			return _currentValue;
		}
		#endregion Implementation of ISettingsObjectReaderSink

		#region Implementation of ISettingsObjectWriterSink
		/// <summary>
		/// Saves the specified settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool WriteSetting<T>(T settings) where T : class
		{
			OnSettingsWritting?.Invoke(this, new SettingsSavingEventArgs() { Data = settings });

			_currentValue = settings;

			return true;
		}
		#endregion ISettingsObjectWriterSink

		#region Implementation of ISettingsValueReaderSink
		public T ReadSetting<T>(string key, T defaultValue = default(T))
		{
			OnSettingsRead?.Invoke(this, new SettingsLoadingEventArgs() { Data = _currentValue });

			return _currentValue[key];
		}
		#endregion Implementation of ISettingsValueReaderSink

		#region Implementation of ISettingsValueWriterSink
		/// <summary>
		/// Writes the specified setting value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool WriteSetting<T>(string key, T value)
		{
			OnSettingsWritting?.Invoke(this, new SettingsSavingEventArgs() { Data = $"{key}={value}" });

			_currentValue[key] = value;

			return true;
		}
		#endregion Implementation of ISettingsValueWriterSink
	}
}
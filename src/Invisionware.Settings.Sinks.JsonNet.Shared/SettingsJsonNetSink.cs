// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.JsonNet
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsJsonNetSink.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;
using Serilog;

namespace Invisionware.Settings.Sinks
{
	/// <summary>
	/// Class SettingsJsonSink.
	/// </summary>
	/// <seealso cref="ISettingsReaderSinkAsync" />
	/// <seealso cref="ISettingsWriterSinkAsync" />
	public class SettingsJsonSink : ISettingsReaderSink, ISettingsWriterSink
	{
		#region Member Variables
		/// <summary>
		/// The file name
		/// </summary>
		private readonly string _fileName;

		private readonly Newtonsoft.Json.JsonSerializerSettings _jsonSettings;
		#endregion Member Variables

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsJsonSink" /> class.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="jsonSettings">The settings.</param>
		public SettingsJsonSink(string fileName, Newtonsoft.Json.JsonSerializerSettings jsonSettings)
		{
			_fileName = fileName;
			_jsonSettings = jsonSettings;
		}

		#region Event Handlers
		public EventHandler<SettingsLoadingEventArgs> OnSettingsLoading { get; set; }
		public EventHandler<SettingsSavingEventArgs> OnSettingsSaving { get; set; }
		#endregion Event Handlers

		#region Implementation of ISettingsSink
		public bool Open()
		{
			return true;
		}

		public Task<bool> OpenAsync() { return Task.FromResult(Open()); }

		public bool Flush() { return true; }

		public Task<bool> FlushAsync() { return Task.FromResult(Flush()); }

		public bool Close() { return true; }

		public Task<bool> CloseAsync() { return Task.FromResult(Close()); }
		#endregion

		#region Implementation of ISettingsReaderSinkAsync
		/// <summary>
		/// Saves the specified settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool Save<T>(T settings) where T : class
		{
			var str = Newtonsoft.Json.JsonConvert.SerializeObject(settings, _jsonSettings);
			Log.Debug($"Saving JsonNet Settings Data: {str}");
			OnSettingsSaving?.Invoke(this, new SettingsSavingEventArgs() { Data = str });

			if (System.IO.File.Exists(_fileName))
			{
				System.IO.File.Delete(_fileName);
			}

			System.IO.File.WriteAllText(_fileName, str);

			return true;
		}

		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T Load<T>() where T : class
		{
			if (!System.IO.File.Exists(_fileName))
			{
				return default(T);
			}

			var str = System.IO.File.ReadAllText(_fileName);
			Log.Debug($"Loading JsonNet Settings Data: {str}");
			OnSettingsLoading?.Invoke(this, new SettingsLoadingEventArgs() { Data = str });

			var settings = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str, _jsonSettings);

			return settings;
		}

		#endregion
	}
}
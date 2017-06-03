// ***********************************************************************
// Assembly         : Invisionware.Settings.Portable
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsMgr.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;
using Invisionware.Settings.Sinks;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsMgr.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="Invisionware.Settings.ISettingsMgr{T}" />
	public class SettingsMgr<T> : ISettingsMgr<T> where T : class, new()
	{
		#region Member Variables
		/// <summary>
		/// The reader sink
		/// </summary>
		private readonly ISettingsReaderSink _readerSink;
		/// <summary>
		/// The writer sink
		/// </summary>
		private readonly ISettingsWriterSink _writerSink;

		/// <summary>
		/// The enrichers
		/// </summary>
		private readonly IList<ISettingsOverride<T>> _overrideEnrichersReaders;
		/// <summary>
		/// The enrichers writers
		/// </summary>
		private readonly IList<ISettingsOverride<T>> _overrideEnrichersWriters;
		#endregion Member Variables

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsMgr{T}"/> class.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="writer">The writer.</param>
		/// <param name="overrideEnrichersReader">The override enrichers reader.</param>
		/// <param name="overrideEnrichersWriters">The override enrichers writers.</param>
		internal SettingsMgr(ISettingsReaderSink reader, ISettingsWriterSink writer, IList<ISettingsOverride<T>> overrideEnrichersReader, IList<ISettingsOverride<T>> overrideEnrichersWriters)
		{
			_readerSink = reader;
			_writerSink = writer;
			_overrideEnrichersReaders = overrideEnrichersReader;
			_overrideEnrichersWriters = overrideEnrichersWriters;
		}

		/// <summary>
		/// Loads the settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T LoadSettings() 
		{
			if (_readerSink == null) throw new NullReferenceException("Reader Sink not set");

			_readerSink.Open();

			var result = _readerSink.Load<T>();

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersReaders)
				{
					e.Enrich(result);
				}
			}

			return result;
		}

		/// <summary>
		/// Loads the settings asynchronous.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Task&lt;T&gt;.</returns>
		public async Task<T> LoadSettingsAsync()
		{
			if (_readerSink == null) throw new NullReferenceException("Reader Sink not set");

			var openResult = _readerSink is ISettingsReaderSinkAsync
				? await ((ISettingsReaderSinkAsync) _readerSink).OpenAsync()
				: _readerSink.Open();

			var result = _readerSink is ISettingsReaderSinkAsync
				? await ((ISettingsReaderSinkAsync) _readerSink).LoadAsync<T>()
				: _readerSink.Load<T>();

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersReaders)
				{
					e.Enrich(result);
				}
			}

			return result;
		}

		/// <summary>
		/// Saves the settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool SaveSettings(T settings) 
		{
			if (_writerSink == null) throw new NullReferenceException("Writer Sink not set");

			_writerSink.Open();

			if (_overrideEnrichersWriters != null && _overrideEnrichersWriters.Count > 0)
			{
				foreach (var e in _overrideEnrichersWriters)
				{
					e.Enrich(settings);
				}
			}

			var result = _writerSink.Save(settings);

			return result;
		}

		/// <summary>
		/// Saves the settings asynchronous.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public async Task<bool> SaveSettingsAsync(T settings)
		{
			if (_writerSink == null) throw new NullReferenceException("Writer Sink not set");

			var openResult = _writerSink is ISettingsWriterSinkAsync
				? await ((ISettingsWriterSinkAsync)_writerSink).OpenAsync()
				: _writerSink.Open();

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersWriters)
				{
					e.Enrich(settings);
				}
			}

			var result = _writerSink is ISettingsWriterSinkAsync
				? await ((ISettingsWriterSinkAsync) _writerSink).SaveAsync(settings)
				: _writerSink.Save(settings);

			return result;
		}
	}
}
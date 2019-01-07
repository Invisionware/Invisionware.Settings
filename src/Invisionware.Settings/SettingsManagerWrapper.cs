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
using System.Linq;
using System.Threading.Tasks;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsMgr.
	/// </summary>
	public sealed class SettingsManagerWrapper : ISettingsObjectMgr, ISettingsObjectMgrAsync, ISettingsValueMgr, ISettingsValueMgrAsync
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
		private readonly IList<ISettingsOverride> _overrideEnrichersReaders;
		/// <summary>
		/// The enrichers writers
		/// </summary>
		private readonly IList<ISettingsOverride> _overrideEnrichersWriters;
		#endregion Member Variables		

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsMgr{T}"/> class.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="writer">The writer.</param>
		/// <param name="overrideEnrichersReader">The override enrichers reader.</param>
		/// <param name="overrideEnrichersWriters">The override enrichers writers.</param>
		internal SettingsManagerWrapper(ISettingsReaderSink reader, ISettingsWriterSink writer, IList<ISettingsOverride> overrideEnrichersReader, IList<ISettingsOverride> overrideEnrichersWriters)
		{
			_readerSink = reader;
			_writerSink = writer;
			_overrideEnrichersReaders = overrideEnrichersReader;
			_overrideEnrichersWriters = overrideEnrichersWriters;
		}
		#endregion Constructors

		#region Settings Name/Value Pairs

		#region Indexers
		public string this[string key] => ReadSetting<string>(key, string.Empty);
		#endregion Indexers

		public T ReadSetting<T>(string key, T defaultValue = default(T))
		{
			if (_readerSink == null) throw new NullReferenceException("Reader Sink not set");
			if (!(_readerSink is ISettingsValueReaderSink)) { throw new InvalidCastException("Reader does not support reading name/value pairs"); }

			_readerSink.Open();

			var result = ((ISettingsValueReaderSink)_readerSink).ReadSetting<T>(key, defaultValue);

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersReaders.Cast<ISettingsValueOverride>())
				{
					result = e.Enrich(key, result);
				}
			}

			return result;
		}

		public async Task<T> ReadSettingAsync<T>(string key, T defaultValue = default(T))
		{
			if (_readerSink == null) throw new NullReferenceException("Reader Sink not set");
			if (!(_readerSink is ISettingsValueReaderSinkAsync)) { throw new InvalidCastException("Reader does not support reading name/value pairs"); }

			var openResult = await ((ISettingsValueReaderSinkAsync)_readerSink).OpenAsync();

			var result = await ((ISettingsValueReaderSinkAsync)_readerSink).ReadSettingAsync<T>(key, defaultValue);

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersReaders.Cast<ISettingsValueOverride>())
				{
					result = e.Enrich(key, result);
				}
			}

			return result;
		}

		public bool WriteSetting<T>(string key, T value)
		{
			if (_writerSink == null) throw new NullReferenceException("Writer Sink not set");
			if (!(_writerSink is ISettingsValueWriterSink)) { throw new InvalidCastException("Reader does not support writting name/value pairs"); }

			_writerSink.Open();

			if (_overrideEnrichersWriters != null && _overrideEnrichersWriters.Count > 0)
			{
				foreach (var e in _overrideEnrichersWriters.Cast<ISettingsValueOverride>())
				{
					value = e.Enrich<T>(key, value);
				}
			}

			var result = ((ISettingsValueWriterSink)_writerSink).WriteSetting(key, value);

			return result;

		}

		public async Task<bool> WriteSettingAsync<T>(string key, T value)
		{
			if (_writerSink == null) throw new NullReferenceException("Writer Sink not set");
			if (!(_writerSink is ISettingsValueWriterSinkAsync)) { throw new InvalidCastException("Reader does not support writting name/value pairs"); }

			var openResult = await ((ISettingsValueWriterSinkAsync)_writerSink).OpenAsync();

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersWriters.Cast<ISettingsValueOverride>())
				{
					value = e.Enrich(key, value);
				}
			}

			var result = await ((ISettingsValueWriterSinkAsync)_writerSink).WriteSettingAsync<T>(key, value);

			return result;
		}
		#endregion Settings Name/Value Pairs

		#region Settings Object
		/// <summary>
		/// Readings the settings.
		/// </summary>
		/// <typeparam name="TSettingsClass"></typeparam>
		/// <returns>T.</returns>
		public TSettingsClass ReadSettings<TSettingsClass>() where TSettingsClass : class, new()
		{
			if (_readerSink == null) throw new NullReferenceException("Reader Sink not set");
			if (!(_readerSink is ISettingsObjectReaderSink)) { throw new InvalidCastException("Reader does not support reading objects"); }

			_readerSink.Open();

			var result = ((ISettingsObjectReaderSink)_readerSink).ReadSetting<TSettingsClass>();

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersReaders.Cast<ISettingsObjectOverride<TSettingsClass>>())
				{
					result = e.Enrich(result);
				}
			}

			return result;
		}

		/// <summary>
		/// Reads the settings asynchronously.
		/// </summary>
		/// <typeparam name="TSettingsClass"></typeparam>
		/// <returns>Task&lt;T&gt;.</returns>
		public async Task<TSettingsClass> ReadSettingsAsync<TSettingsClass>() where TSettingsClass : class, new()
		{
			if (_readerSink == null) throw new NullReferenceException("Reader Sink not set");
			if (!(_readerSink is ISettingsObjectReaderSinkAsync)) { throw new InvalidCastException("Reader does not support reading objects"); }

			var openResult = await ((ISettingsObjectReaderSinkAsync)_readerSink).OpenAsync();

			var result = await ((ISettingsObjectReaderSinkAsync)_readerSink).ReadSettingAsync<TSettingsClass>();

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersReaders.Cast<ISettingsObjectOverride<TSettingsClass>>())
				{
					result = e.Enrich(result);
				}
			}

			return result;
		}

		/// <summary>
		/// Saves the settings.
		/// </summary>
		/// <typeparam name="TSettingsClass"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if sucess, <c>false</c> otherwise.</returns>
		public bool WriteSettings<TSettingsClass>(TSettingsClass settings) where TSettingsClass : class, new()
		{
			if (_writerSink == null) throw new NullReferenceException("Writer Sink not set");
			if (!(_writerSink is ISettingsObjectWriterSink)) { throw new InvalidCastException("Reader does not support writting objects"); }

			_writerSink.Open();

			if (_overrideEnrichersWriters != null && _overrideEnrichersWriters.Count > 0)
			{
				foreach (var e in _overrideEnrichersWriters.Cast<ISettingsObjectOverride<TSettingsClass>>())
				{
					settings = e.Enrich(settings);
				}
			}

			var result = ((ISettingsObjectWriterSink)_writerSink).WriteSetting(settings);

			return result;
		}

		/// <summary>
		/// Saves the settings asynchronous.
		/// </summary>
		/// <typeparam name="TSettingsClass"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public async Task<bool> WriteSettingsAsync<TSettingsClass>(TSettingsClass settings) where TSettingsClass : class, new()
		{
			if (_writerSink == null) throw new NullReferenceException("Writer Sink not set");
			if (!(_writerSink is ISettingsObjectWriterSinkAsync)) { throw new InvalidCastException("Reader does not support writting objects"); }

			var openResult = await ((ISettingsObjectWriterSinkAsync)_writerSink).OpenAsync();

			if (_overrideEnrichersReaders != null && _overrideEnrichersReaders.Count > 0)
			{
				foreach (var e in _overrideEnrichersWriters.Cast<ISettingsObjectOverride<TSettingsClass>>())
				{
					settings = e.Enrich(settings);
				}
			}

			var result = await ((ISettingsObjectWriterSinkAsync)_writerSink).WriteSettingAsync(settings);				

			return result;
		}
		#endregion Settings Object
	}
}
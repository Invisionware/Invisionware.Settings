// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.Azure
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="AzureDocumentDbSink.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Serilog;

namespace Invisionware.Settings.Sinks.Azure
{
	/// <summary>
	/// Class AzureDocumentDbSink.
	/// </summary>
	/// <seealso cref="ISettingsReaderSinkAsync" />
	/// <seealso cref="ISettingsWriterSinkAsync" />
	public class AzureDocumentDbSink : ISettingsObjectReaderSink, ISettingsObjectReaderSinkAsync, ISettingsObjectWriterSink, ISettingsObjectWriterSinkAsync
	{
		#region Member Variables
		private bool _isOpen;

		/// <summary>
		/// The exception mut
		/// </summary>
		private readonly Mutex _exceptionMut = new Mutex();

		/// <summary>
		/// The client
		/// </summary>
		private readonly DocumentClient _client;

		/// <summary>
		/// The collection
		/// </summary>
		private DocumentCollection _collection;

		/// <summary>
		/// The database
		/// </summary>
		private Database _database;

		/// <summary>
		/// The database name
		/// </summary>
		private readonly string _databaseName;

		/// <summary>
		/// The collection name
		/// </summary>
		private readonly string _collectionName;

		/// <summary>
		/// The collection link
		/// </summary>
		private string _collectionLink;

		/// <summary>
		/// The maximum versions
		/// </summary>
		private readonly int _maxVersions;

		private readonly JsonSerializerSettings _jsonSettings;
		#endregion Member Variables

		/// <summary>
		/// Initializes a new instance of the <see cref="AzureDocumentDbSink" /> class.
		/// </summary>
		/// <param name="endpointUri">The endpoint URI.</param>
		/// <param name="authorizationKey">The authorization key.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="collectionName">Name of the collection.</param>
		/// <param name="maxVersions">The maximum versions.</param>
		/// <param name="connectionProtocol">The connection protocol.</param>
		/// <param name="jsonSettings">The json settings.</param>
		public AzureDocumentDbSink(Uri endpointUri, string authorizationKey, string databaseName, string collectionName,
			int maxVersions = 3,
			Protocol connectionProtocol = Protocol.Https, JsonSerializerSettings jsonSettings = null)
		{
			_maxVersions = maxVersions;
			_jsonSettings = jsonSettings;

			_databaseName = databaseName;
			_collectionName = collectionName;

			_client = new DocumentClient(endpointUri,
				authorizationKey,
				new ConnectionPolicy
				{
					ConnectionMode =
						connectionProtocol == Protocol.Https ? ConnectionMode.Gateway : ConnectionMode.Direct,
					ConnectionProtocol = connectionProtocol,
					MaxConnectionLimit = Environment.ProcessorCount * 50 + 200
				});
		}

		#region Event Handlers
		public EventHandler<SettingsLoadingEventArgs> OnSettingsRead { get; set; }
		public EventHandler<SettingsSavingEventArgs> OnSettingsWritting { get; set; }
		#endregion Event Handlers

		public bool Open()
		{
			var task = OpenAsync();

			task.Wait();

			return task.Result;
		}

		public async Task<bool> OpenAsync()
		{
			if (_isOpen) return true;

			await CreateDatabaseIfNotExistsAsync(_databaseName).ConfigureAwait(false);
			await CreateCollectionIfNotExistsAsync(_collectionName).ConfigureAwait(false);

			_isOpen = true;

			return true;
		}

		/// <summary>
		/// Flushes this instance.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool Flush() { return true; }

		/// <summary>
		/// Flushes the instance asynchronously.
		/// </summary>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public Task<bool> FlushAsync() { return Task.FromResult(Flush()); }

		/// <summary>
		/// Closes this instance.
		/// </summary>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool Close() { _isOpen = false; return true;}

		/// <summary>
		/// Closes the instance asynchronously.
		/// </summary>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public Task<bool> CloseAsync() {  return Task.FromResult(Close()); }

		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T ReadSetting<T>() where T : class
		{
			var response = _client.CreateDocumentQuery(_collectionLink).Select(x => x).OrderByDescending(x => x.Timestamp).ToList().FirstOrDefault();
			T result;

			if (response == null) return default(T);

			try
			{
				if (_jsonSettings != null)
				{
					var str = response.ToString();

					Log.Debug($"Loading Azure DocumentDb Settings Data (1): {str}");
					OnSettingsRead?.Invoke(this, new SettingsLoadingEventArgs() { Data = str });

					result = JsonConvert.DeserializeObject<T>(str, _jsonSettings);
				}
				else
				{
					result = (T)(dynamic)response;
				}
			}
			catch (Exception e) when (_jsonSettings != null)
			{
				Log.Warning(e, "Azure DocumentDb Failed to Load");

				try
				{
					var str = response.ToString();

					Log.Debug($"Loading Azure DocumentDb Settings Data (2): {str}");

					result = JsonConvert.DeserializeObject<T>(str, _jsonSettings);
				}
				catch
				{
					throw e;
				}
			}

			return result;
		}

		/// <summary>
		/// Loads the asynchronous.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Task&lt;T&gt;.</returns>
		public Task<T> ReadSettingAsync<T>() where T : class
		{
			var result = ReadSetting<T>();

			return Task.FromResult(result);
		}

		/// <summary>
		/// Saves the specified settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool WriteSetting<T>(T settings) where T : class
		{
			var task = WriteSettingAsync(settings);
			task.Wait();

			return task.Result;
		}

		/// <summary>
		/// save as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public async Task<bool> WriteSettingAsync<T>(T settings) where T : class
		{
			bool result = false;

			try
			{
				OnSettingsWritting?.Invoke(this, new SettingsSavingEventArgs() { Data = settings });
				var response = await _client.UpsertDocumentAsync(_collectionLink, settings).ConfigureAwait(false);

				result = response.Resource != null;

				// Lets check and see if we have too many versions, if so lets get rid of the old ones
				await PurgeOldVersionsAsync();
			}
			catch (AggregateException e)
			{
				var exception = e.InnerException as DocumentClientException;

				if (exception != null)
				{
					if (exception.StatusCode == null)
					{
						var ei = (DocumentClientException)e.InnerException;

						if (ei?.StatusCode != null)
						{
							exception = ei;
						}
					}
				}

				try
				{
					_exceptionMut.WaitOne();

					if (exception?.StatusCode != null)
						switch ((int)exception.StatusCode)
						{
							case 429:
								var delayTask = Task.Delay(TimeSpan.FromMilliseconds(exception.RetryAfter.Milliseconds));
								delayTask.Wait();
								break;
							default:
								break;
						}
				}
				finally
				{
					var response = await _client.UpsertDocumentAsync(_collectionLink, settings).ConfigureAwait(false);

					_exceptionMut.ReleaseMutex();

					result = response.Resource != null;
				}
			}

			return result;
		}

		/// <summary>
		/// create database if not exists as an asynchronous operation.
		/// </summary>
		/// <param name="databaseName">Name of the database.</param>
		/// <returns>Task.</returns>
		private async Task CreateDatabaseIfNotExistsAsync(string databaseName)
		{
//			SelfLog.WriteLine($"Opening database {databaseName}");

			await _client.OpenAsync();

			_database = _client
				            .CreateDatabaseQuery()
				            .Where(x => x.Id == databaseName)
				            .AsEnumerable()
				            .FirstOrDefault() ?? await _client
				            .CreateDatabaseAsync(new Database { Id = databaseName })
				            .ConfigureAwait(false);
		}

		/// <summary>
		/// create collection if not exists as an asynchronous operation.
		/// </summary>
		/// <param name="collectionName">Name of the collection.</param>
		/// <returns>Task.</returns>
		private async Task CreateCollectionIfNotExistsAsync(string collectionName)
		{
			//SelfLog.WriteLine($"Creating collection: {collectionName}");

			_collection =
				_client.CreateDocumentCollectionQuery(_database.SelfLink)
					.Where(x => x.Id == collectionName)
					.AsEnumerable()
					.FirstOrDefault();

			if (_collection == null)
			{
				var documentCollection = new DocumentCollection {Id = collectionName, DefaultTimeToLive = -1};

				_collection = await _client.CreateDocumentCollectionAsync(_database.SelfLink, documentCollection)
					.ConfigureAwait(false);
			}

			_collectionLink = _collection.SelfLink;
		}

		/// <summary>
		/// Purges the old versions.
		/// </summary>
		/// <returns>Task.</returns>
		private async Task PurgeOldVersionsAsync()
		{
			try
			{
				var documents = _client.CreateDocumentQuery(_collectionLink)
					.OrderByDescending(x => x.Timestamp)
					.Select(x => x)
					.ToList();

				foreach (var d in documents.Skip(_maxVersions))
				{
					await _client.DeleteDocumentAsync(d.SelfLink).ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"AzureDocumentDbSink - Failed to remove old version. EndPoint='{this._client.ServiceEndpoint}', CollectionLink='{_collectionLink}'");
			}
		}
	}
}

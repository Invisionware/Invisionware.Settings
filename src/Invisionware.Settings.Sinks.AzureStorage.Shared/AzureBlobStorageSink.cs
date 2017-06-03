// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.AzureBlobStorage
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="AzureBlobStorageSink.cs" company="Invisionware.Settings.Sinks.AzureBlobStorage">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Serilog;

namespace Invisionware.Settings.Sinks.Azure
{
	/// <summary>
	/// Class AzureBlobStorageSink.
	/// </summary>
	/// <seealso cref="ISettingsReaderSinkAsync" />
	/// <seealso cref="ISettingsWriterSinkAsync" />
	public class AzureBlobStorageSink : ISettingsReaderSinkAsync, ISettingsWriterSinkAsync
	{
		#region Member Variables
		private bool _isOpen;

		/// <summary>
		/// The storage account
		/// </summary>
		private readonly CloudStorageAccount _storageAccount;
		/// <summary>
		/// The BLOB client
		/// </summary>
		private readonly CloudBlobClient _blobClient;
		/// <summary>
		/// The BLOB container
		/// </summary>
		private readonly CloudBlobContainer _blobContainer;

		/// <summary>
		/// The file name
		/// </summary>
		private readonly string _fileName;
		#endregion Member Variables

		/// <summary>
		/// Initializes a new instance of the <see cref="AzureBlobStorageSink"/> class.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="containerName">Name of the container.</param>
		/// <param name="fileName">Name of the file.</param>
		public AzureBlobStorageSink(string connectionString, string containerName, string fileName)
		{
			_fileName = fileName;

			_storageAccount = CloudStorageAccount.Parse(connectionString);

			_blobClient = _storageAccount.CreateCloudBlobClient();

			_blobContainer = _blobClient.GetContainerReference(containerName.ToLower());
		}

		#region Event Handlers
		public EventHandler<SettingsLoadingEventArgs> OnSettingsLoading { get; set; }
		public EventHandler<SettingsSavingEventArgs> OnSettingsSaving { get; set; }
		#endregion Event Handlers

		#region Implementation of ISettingsSink
		public bool Open()
		{
			var task = OpenAsync();

			task.Wait();

			return task.Result;
		}

		public Task<bool> OpenAsync()
		{
			if (_isOpen) return Task.FromResult(true);

			return _blobContainer.CreateIfNotExistsAsync().ContinueWith(t =>
			{
				_isOpen = t.Result;

				return _isOpen;
			});
		}

		public bool Flush() { return true; }

		public Task<bool> FlushAsync() { return Task.FromResult(Flush()); }

		public bool Close() { _isOpen = false; return true; }

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
			SaveAsync(settings).Wait();

			return true;
		}

		/// <summary>
		/// save as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public async Task<bool> SaveAsync<T>(T settings) where T : class
		{
			try
			{
				var blobBlock = _blobContainer.GetBlockBlobReference(_fileName);

				var str = Newtonsoft.Json.JsonConvert.SerializeObject(settings);

				Log.Debug($"Saving Azure DocumentDb Settings Data: {str}");
				OnSettingsSaving?.Invoke(this, new SettingsSavingEventArgs() { Data = str });

				await blobBlock.DeleteIfExistsAsync();
				
				await blobBlock.UploadTextAsync(str).ConfigureAwait(false);

				return true;
			}
			finally
			{
				
			}
		}

		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T Load<T>() where T : class
		{
			var task = LoadAsync<T>();

			task.Wait();

			return task.Result;
		}

		/// <summary>
		/// load as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Task&lt;T&gt;.</returns>
		public async Task<T> LoadAsync<T>() where T : class
		{
			try
			{
				var blobBlock = _blobContainer.GetBlockBlobReference(_fileName);

				if (await blobBlock.ExistsAsync())
				{
					var str = await blobBlock.DownloadTextAsync();

					Log.Debug($"Loading Azure DocumentDb Settings Data (1): {str}");
					OnSettingsLoading?.Invoke(this, new SettingsLoadingEventArgs() { Data = str });

					var settings = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);

					return settings;
				}

				return null;
			}
			finally
			{

			}
		}

		#endregion
	}
}
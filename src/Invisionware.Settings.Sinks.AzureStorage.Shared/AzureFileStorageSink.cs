using System;
using System.Configuration;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Serilog;

namespace Invisionware.Settings.Sinks.Azure
{
	public class AzureFileStorageSink : ISettingsReaderSinkAsync, ISettingsWriterSinkAsync
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
		private readonly CloudFileClient _fileClient;
		/// <summary>
		/// The BLOB container
		/// </summary>
		private readonly CloudFileShare _cloudFileShare;

		private CloudFileDirectory _cloudRootDirectory;

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
		public AzureFileStorageSink(string connectionString, string containerName, string fileName)
		{
			_fileName = fileName;

			_storageAccount = CloudStorageAccount.Parse(connectionString);

			_fileClient = _storageAccount.CreateCloudFileClient();
			
			_cloudFileShare = _fileClient.GetShareReference(containerName.ToLower());
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

		public async Task<bool> OpenAsync()
		{
			if (_isOpen) return true;

			var result = await _cloudFileShare.CreateIfNotExistsAsync().ConfigureAwait(false);

			_cloudRootDirectory = _cloudFileShare.GetRootDirectoryReference();

			//// Create a new shared access policy and define its constraints.
			//var sharedPolicy = new SharedAccessFilePolicy()
			//{
			//	SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
			//	Permissions = SharedAccessFilePermissions.Read | SharedAccessFilePermissions.Write
			//};

			//// Get existing permissions for the share.
			//var permissions = _cloudFileShare.GetPermissions();

			//if (!permissions.SharedAccessPolicies.ContainsKey("SettingsSharePolicy"))
			//{
			//	// Add the shared access policy to the share's policies. Note that each policy must have a unique name.
			//	permissions.SharedAccessPolicies.Add("SettingsSharePolicy", sharedPolicy);
			//	_cloudFileShare.SetPermissions(permissions);
			//}

			_isOpen = true;

			return true;
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
				var file = _cloudRootDirectory.GetFileReference(_fileName);

				var str = Newtonsoft.Json.JsonConvert.SerializeObject(settings);

				Log.Debug($"Saving Azure DocumentDb Settings Data (1): {str}");
				OnSettingsSaving?.Invoke(this, new SettingsSavingEventArgs() {Data = str});

				await file.DeleteIfExistsAsync();

				await file.UploadTextAsync(str).ConfigureAwait(false);

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
				var file = _cloudRootDirectory.GetFileReference(_fileName);

				if (await file.ExistsAsync())
				{
					var str = await file.DownloadTextAsync();

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
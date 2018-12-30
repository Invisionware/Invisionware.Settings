using System;
using System.Configuration;
using System.Threading.Tasks;
using Invisionware.Settings.EventArgs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Serilog;

namespace Invisionware.Settings.Sinks.Azure
{
	/// <summary>
	/// Class AzureFileStorageSink.
	/// </summary>
	/// <seealso cref="ISettingsObjectReaderSinkAsync" />
	/// <seealso cref="ISettingsObjectWriterSinkAsync" />
	public class AzureFileStorageSink : ISettingsObjectReaderSinkAsync, ISettingsObjectWriterSinkAsync
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
		public EventHandler<SettingsLoadingEventArgs> OnSettingsRead { get; set; }
		public EventHandler<SettingsSavingEventArgs> OnSettingsWritting { get; set; }
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

		#region Implementation of ISettingsObjectReaderSink
		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>T.</returns>
		public T ReadSetting<T>() where T : class
		{
			var task = ReadSettingAsync<T>();

			task.Wait();

			return task.Result;
		}
		#endregion Implementation of ISettingsObjectReaderSink

		#region Implementation of ISettingsObjectReaderSinkAsync
		/// <summary>
		/// load as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Task&lt;T&gt;.</returns>
		public async Task<T> ReadSettingAsync<T>() where T : class
		{
			try
			{
				var file = _cloudRootDirectory.GetFileReference(_fileName);

				if (await file.ExistsAsync())
				{
					var str = await file.DownloadTextAsync();

					Log.Debug($"Loading Azure DocumentDb Settings Data (1): {str}");
					OnSettingsRead?.Invoke(this, new SettingsLoadingEventArgs() { Data = str });

					var settings = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);

					return settings;
				}

				return null;
			}
			finally
			{

			}
		}
		#endregion Implementation of ISettingsObjectReaderSinkAsync

		#region Implementation of ISettingsObjectWritterSink
		/// <summary>
		/// Saves the specified settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool WriteSetting<T>(T settings) where T : class
		{
			WriteSettingAsync(settings).Wait();

			return true;
		}
		#endregion Implementation of ISettingsObjectWritterSink

		#region Implementation of ISettingsObjectWritterSinkAsync
		/// <summary>
		/// save as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public async Task<bool> WriteSettingAsync<T>(T settings) where T : class
		{
			try
			{
				var file = _cloudRootDirectory.GetFileReference(_fileName);

				var str = Newtonsoft.Json.JsonConvert.SerializeObject(settings);

				Log.Debug($"Saving Azure DocumentDb Settings Data (1): {str}");
				OnSettingsWritting?.Invoke(this, new SettingsSavingEventArgs() {Data = str});

				await file.DeleteIfExistsAsync();

				await file.UploadTextAsync(str).ConfigureAwait(false);

				return true;
			}
			finally
			{

			}
		}
		#endregion ISettingsObjectWritterSinkAsync
	}
}
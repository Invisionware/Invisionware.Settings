using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Invisionware.Settings.Sinks;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Invisionware.Settings.Tests
{
	[TestFixture]
	[Category("Settings")]
	[Category("Settings.Sink.AzureDocumentDb")]
	public class SinkAzureDocumentDbTests
	{
		public void AzureDocumentDbAppSettingsConstructor()
		{
			var settingsConfig = new SettingsConfiguration().WriteTo.AzureDocumentDb().ReadFrom.AzureDocumentDb();			
		}

		[Test]
		public void AzureDocumentDbJsonSerializerTest()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthorizationKey"]; 

			var jsonSettings = new JsonSerializerSettings()
			{
				DateFormatHandling = DateFormatHandling.IsoDateFormat,
				Formatting = Formatting.Indented
			};

			var settingsConfig = new SettingsConfiguration().WriteTo.AzureDocumentDb(endPointUri, authKey, jsonSettings: jsonSettings).ReadFrom.AzureDocumentDb(endPointUri, authKey, jsonSettings: jsonSettings);

			settingsConfig.OnSettingsSaving += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			settingsConfig.OnSettingsLoading += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsObjectMgr>();
			settingsMgr.Should().NotBeNull();
			settingsMgr.Should().BeAssignableTo<ISettingsObjectMgr>();

			var settings = new CustomSettings();

			settingsMgr.WriteSettings(settings);

			settings.String1 = "Test1";

			var settingsNew = settingsMgr.ReadSettings<CustomSettings>();

			settings.String1.Should().NotBe(settingsNew.String1);
		}

		[Test]
		public void AzureDocumentDbTest()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthorizationKey"];

			var settingsConfig = new SettingsConfiguration().WriteTo.AzureDocumentDb(endPointUri, authKey).ReadFrom.AzureDocumentDb(endPointUri, authKey);

			settingsConfig.OnSettingsSaving += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			settingsConfig.OnSettingsLoading += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsObjectMgr>();
			settingsMgr.Should().NotBeNull();
			settingsMgr.Should().BeAssignableTo<ISettingsObjectMgr>();

			var settings = new CustomSettings();

			settingsMgr.WriteSettings(settings);

			settings.String1 = "Test1";

			var settingsNew = settingsMgr.ReadSettings<CustomSettings>();

			settings.String1.Should().NotBe(settingsNew.String1);
		}

		[Test]
		public async Task AzureDocumentDbTestAsync()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthorizationKey"];

			var settingsConfig = new SettingsConfiguration().WriteTo.AzureDocumentDb(endPointUri, authKey).ReadFrom.AzureDocumentDb(endPointUri, authKey);

			settingsConfig.OnSettingsSaving += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			settingsConfig.OnSettingsLoading += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsObjectMgrAsync>();
			settingsMgr.Should().NotBeNull();
			settingsMgr.Should().BeAssignableTo<ISettingsObjectMgrAsync>();

			var settings = new CustomSettings();

			await settingsMgr.WriteSettingsAsync(settings);

			settings.String1 = "Test1";

			var settingsNew = await settingsMgr.ReadSettingsAsync<CustomSettings>();

			settings.String1.Should().NotBe(settingsNew.String1);
		}

		[Test]
		public async Task AzureDocumentDbMaxVersionsTest()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthorizationKey"];
			var databaseName = "AppSettings";
			var collectionName = "Config";
			int maxVersions = 3;

			var settingsConfig = new SettingsConfiguration().WriteTo
				.AzureDocumentDb(endPointUri, authKey, maxVersions: maxVersions, databaseName: databaseName,
					collectionName: collectionName)
				.ReadFrom.AzureDocumentDb(endPointUri, authKey);

			settingsConfig.OnSettingsSaving += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			settingsConfig.OnSettingsLoading += (sender, args) =>
			{
				args.Should().NotBeNull();
				args.Data.Should().NotBeNull();
			};

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsObjectMgr>();
			settingsMgr.Should().NotBeNull();
			settingsMgr.Should().BeAssignableTo<ISettingsObjectMgr>();

			var settings = new CustomSettings();

			for (int i = 0; i <= maxVersions; i++)
			{
				settings.Int1 = i;

				settingsMgr.WriteSettings(settings);
			}

			var client = new DocumentClient(endPointUri,
				authKey);

			await client.OpenAsync();

			var database = client
				.CreateDatabaseQuery()
				.Where(x => x.Id == databaseName)
				.AsEnumerable()
				.FirstOrDefault();

			var collection =
				client.CreateDocumentCollectionQuery(database.SelfLink)
					.Where(x => x.Id == collectionName)
					.AsEnumerable()
					.FirstOrDefault();

			var documents = client.CreateDocumentQuery(collection.SelfLink)
				.OrderByDescending(x => x.Timestamp)
				.Select(x => x)
				.ToList();

			documents.Count.Should().BeLessOrEqualTo(maxVersions);
		}
	}
}

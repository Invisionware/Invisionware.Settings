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
	[Category("Settings.AzureDocumentDb")]
	public class AzureDocumentDbSinkTests
	{
		public void AzureDocumentDbAppSettingsConstructor()
		{
			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.AzureDocumentDb().ReadFrom.AzureDocumentDb();			
		}

		[Test]
		public void AzureDocumentDbJsonSerializerTest()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthKey"]; 

			var jsonSettings = new JsonSerializerSettings()
			{
				DateFormatHandling = DateFormatHandling.IsoDateFormat,
				Formatting = Formatting.Indented
			};

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.AzureDocumentDb(endPointUri, authKey, jsonSettings: jsonSettings).ReadFrom.AzureDocumentDb(endPointUri, authKey, jsonSettings: jsonSettings);

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

			var settingsMgr = settingsConfig.CreateSettingsMgr();

			var settings = new CustomSettings();

			settingsMgr.SaveSettings(settings);

			settings.String1 = "Test1";

			var settingsNew = settingsMgr.LoadSettings();

			settings.String1.Should().NotBe(settingsNew.String1);
		}

		[Test]
		public void AzureDocumentDbTest()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthKey"];

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.AzureDocumentDb(endPointUri, authKey).ReadFrom.AzureDocumentDb(endPointUri, authKey);

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

			var settingsMgr = settingsConfig.CreateSettingsMgr();

			var settings = new CustomSettings();

			settingsMgr.SaveSettings(settings);

			settings.String1 = "Test1";

			var settingsNew = settingsMgr.LoadSettings();

			settings.String1.Should().NotBe(settingsNew.String1);
		}

		[Test]
		public async Task AzureDocumentDbTestAsync()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthKey"];

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.AzureDocumentDb(endPointUri, authKey).ReadFrom.AzureDocumentDb(endPointUri, authKey);

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

			var settingsMgr = settingsConfig.CreateSettingsMgr();

			var settings = new CustomSettings();

			await settingsMgr.SaveSettingsAsync(settings);

			settings.String1 = "Test1";

			var settingsNew = await settingsMgr.LoadSettingsAsync();

			settings.String1.Should().NotBe(settingsNew.String1);
		}

		[Test]
		public async Task AzureDocumentDbMaxVersionsTest()
		{
			var endPointUri = new Uri(ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:EndPoint"]);
			var authKey = ConfigurationManager.AppSettings["settings:sink:AzureDocumentDb:AuthKey"];
			var databaseName = "AppSettings";
			var collectionName = "Config";
			int maxVersions = 3;

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo
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

			var settingsMgr = settingsConfig.CreateSettingsMgr();

			var settings = new CustomSettings();

			for (int i = 0; i <= maxVersions; i++)
			{
				settings.Int1 = i;

				settingsMgr.SaveSettings(settings);
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

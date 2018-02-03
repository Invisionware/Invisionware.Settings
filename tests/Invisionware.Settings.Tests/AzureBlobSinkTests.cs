using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Invisionware.Settings.Sinks;
using NUnit.Framework;

namespace Invisionware.Settings.Tests
{
	[TestFixture]
	[Category("Settings")]
	[Category("Settings.AzureBlob")]
	public class AzureBlobSinkTests
	{
		[Test]
		public void AzureBlobTest()
		{
			var connectionString = ConfigurationManager.AppSettings["settings:sink:AzureBlob:ConnectionString"];

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.AzureBlobStorage(connectionString).ReadFrom.AzureBlobStorage(connectionString);

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
		public async Task AzureBlobTestAsync()
		{
			var connectionString = ConfigurationManager.AppSettings["settings:sink:AzureBlob:ConnectionString"];

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.AzureBlobStorage(connectionString).ReadFrom.AzureBlobStorage(connectionString);

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
	}
}

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
	[Category("Settings.Sink.AzureFile")]
	[Ignore("Missing Configuration")]
	public class SinkAzureFileTests
	{
		//[Test]
		//public void AzureFileTest()
		//{
		//	var connectionString = ConfigurationManager.AppSettings["settings:sink:AzureFile:ConnectionString"];

		//	var settingsConfig = new SettingsConfiguration().WriteTo.AzureFileStorage(connectionString)
		//		.ReadFrom.AzureFileStorage(connectionString);

		//	settingsConfig.OnSettingsSaving += (sender, args) =>
		//	{
		//		args.Should().NotBeNull();
		//		args.Data.Should().NotBeNull();
		//	};

		//	settingsConfig.OnSettingsLoading += (sender, args) =>
		//	{
		//		args.Should().NotBeNull();
		//		args.Data.Should().NotBeNull();
		//	};

		//	var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsObjectMgr>();
		//	settingsMgr.Should().NotBeNull();
		//	settingsMgr.Should().BeAssignableTo<ISettingsObjectMgr>();

		//	var settings = new CustomSettings();

		//	settingsMgr.WriteSettings(settings);

		//	settings.String1 = "Test1";

		//	var settingsNew = settingsMgr.ReadSettings<CustomSettings>();

		//	settings.String1.Should().NotBe(settingsNew.String1);
		//}

		[Test]
		public async Task AzureFileTestAsync()
		{
			var connectionString = ConfigurationManager.AppSettings["settings:sink:AzureFile:ConnectionString"];

			var settingsConfig = new SettingsConfiguration().WriteTo.AzureFileStorage(connectionString)
				.ReadFrom.AzureFileStorage(connectionString);

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
	}
}

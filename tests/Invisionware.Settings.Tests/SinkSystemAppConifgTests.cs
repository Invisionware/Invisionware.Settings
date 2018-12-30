using System.Configuration;
using FluentAssertions;
using Invisionware.Settings.Sinks.SystemAppConfig;
using NUnit.Framework;

namespace Invisionware.Settings.Tests
{
	[TestFixture]
	[Category("Settings")]
	[Category("Settings.Sink.SystemAppConfig")]
	public class SinkSystemAppConifgTests
	{
		[Test]
		public void AppConfigSettingsMgrValidKayTest()
		{
			var settingsConfig = new SettingsConfiguration().ReadFrom.SystemAppConfig();

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

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsValueMgr>();
			settingsMgr.Should().NotBeNull();
			settingsMgr.Should().BeAssignableTo<ISettingsValueMgr>();

			var settingsNew = settingsMgr.ReadSetting("TestKey1", "");

			settingsNew.Should().Be("TestValue1");
		}

		[Test]
		public void AppConfigSettingsMgrInValidKeyTest()
		{
			var settingsConfig = new SettingsConfiguration().ReadFrom.SystemAppConfig();

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsValueMgr>();
			settingsMgr.Should().NotBeNull();
			settingsMgr.Should().BeAssignableTo<ISettingsValueMgr>();

			var settingsNew = settingsMgr.ReadSetting("TestKey1_Invalid", "");

			settingsNew.Should().BeNullOrEmpty();
		}

		[Test]
		public void AppConfigSettingsMgrDefaultValueTest()
		{
			var settingsConfig = new SettingsConfiguration().ReadFrom.SystemAppConfig();

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsValueMgr>();
			settingsMgr.Should().NotBeNull();
			settingsMgr.Should().BeAssignableTo<ISettingsValueMgr>();

			var settingsNew = settingsMgr.ReadSetting("TestKey1_DefaultValue", "Default Value");

			settingsNew.Should().Be("Default Value");
		}
	}
}

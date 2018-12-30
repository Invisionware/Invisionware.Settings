using System.Configuration;
using FluentAssertions;
using Invisionware.Settings.Sinks;
using NUnit.Framework;

namespace Invisionware.Settings.Tests
{
	[TestFixture]
	[Category("Settings")]
	[Category("Settings.Sink.ConfigBuilder")]
	public class SinkConfigBuilderTests
	{
		[Test]
		public void ConfigBuilderSettingsMgrValidKayTest()
		{
			var settingsConfig = new SettingsConfiguration().ReadFrom.ConfigBuilder();

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

			var settings = settingsMgr.ReadSettings<CustomSettings>();

			settings.Should().NotBeNull();
			settings.Should().BeOfType<CustomSettings>();
			settings.String1.Should().Be("String1");

		}

		//[Test]
		//public void ConfigBuilderSettingsMgrInValidKeyTest()
		//{
		//	var configMgr = new ConfigBuilderSettingsMgr();

		//	configMgr.LoadSettings();

		//	var result = configMgr.GetValue<string>("TestKey1_Invalid");

		//	result.Should().BeNullOrEmpty();
		//}

		//[Test]
		//public void ConfigBuilderSettingsMgrDefaultValueTest()
		//{
		//	var configMgr = new ConfigBuilderSettingsMgr();

		//	configMgr.LoadSettings();

		//	var result = configMgr.GetValue("TestKey1_DefaultValue", "Default Value");

		//	result.Should().Be("Default Value");
		//}

		//[Test]
		//public void ConfigBuilderSettingsMgrSetDefaultValidTest()
		//{
		//	AppConfigSettingsMgr.Register(new ConfigBuilderSettingsMgr());            

		//	var result = AppConfigSettingsMgr.GetValue<string>("TestKey1");

		//	result.Should().Be(ConfigurationManager.AppSettings["TestKey1"]);
		//}
	}
}

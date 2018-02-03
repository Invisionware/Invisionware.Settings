using System.Configuration;
using FluentAssertions;
using Invisionware.Settings.AppConfigSettings.SystemAppConfig;
using NUnit.Framework;

namespace Invisionware.Settings.Tests
{
	[TestFixture]
	[Category("Settings")]
	[Category("Settings.AppConfigMgr")]
	public class AppConfigMgrTests
	{
		[Test]
		public void AppConfigSettingsMgrValidKayTest()
		{
			var configMgr = new LocalAppConfigSettingsMgr();

			var result = configMgr.GetValue<string>("TestKey1");

			result.Should().Be(ConfigurationManager.AppSettings["TestKey1"]);
		}

		[Test]
		public void AppConfigSettingsMgrInValidKeyTest()
		{
			var configMgr = new LocalAppConfigSettingsMgr();

			var result = configMgr.GetValue<string>("TestKey1_Invalid");

			result.Should().BeNullOrEmpty();
		}

		[Test]
		public void AppConfigSettingsMgrDefaultValueTest()
		{
			var configMgr = new LocalAppConfigSettingsMgr();

			var result = configMgr.GetValue("TestKey1_DefaultValue", "Default Value");

			result.Should().Be("Default Value");
		}

		[Test]
		public void AppConfigSettingsMgrSetDefaultValidTest()
		{
			AppConfigSettingsMgr.Register(new LocalAppConfigSettingsMgr());

			var result = AppConfigSettingsMgr.GetValue<string>("TestKey1");

			result.Should().Be(ConfigurationManager.AppSettings["TestKey1"]);
		}
	}
}

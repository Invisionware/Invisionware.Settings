using System;
using System.Collections.Generic;
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
	[Category("Settings.JsonNet")]
	public class JsonNetSinkTests
	{
		[Test]
		public void JsonNetTest()
		{
			var f = System.IO.Path.Combine(TestContext.CurrentContext.WorkDirectory, "customSettings.json");

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.JsonNet(f).ReadFrom.JsonNet(f);

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

			settingsNew.Should().NotBeNull();
			settingsNew.Should().BeOfType<CustomSettings>();

			settings.String1.Should().NotBe(settingsNew.String1);
			settings.Should().BeOfType<CustomSettings>();
		}
	}
}

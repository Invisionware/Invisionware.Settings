using FluentAssertions;
using Invisionware.Settings.Sinks;
using NUnit.Framework;
using System;

namespace Invisionware.Settings.Tests
{
	[TestFixture]
	[Category("SettingsManager")]
	public class SettingsMangerTests
	{
		[SetUp]
		public void Initialize()
		{
			var defaultSettings = new CustomSettings();

			var settingsConfig = new SettingsConfiguration().WriteTo.Memory(defaultSettings)
				.ReadFrom.Memory(defaultSettings)
				.ReaderOverrides.WithAction<CustomSettings>(
					(s) =>
					{
						s.String1 = "Override1";

						return s;
					})
				.WriterOverrides.WithAction<CustomSettings>(
					(s) =>
					{
						return s;
					});

			var settingsMgr = settingsConfig.CreateSettingsMgr<ISettingsObjectMgr>();
			SettingsManager.SetSettingsManager(settingsMgr);
		}

		[Test]
		public void StaticReadSettingsTest()
		{
			var settings = SettingsManager.ReadSettings<CustomSettings>();

			settings.String1.Should().Be("Override1");
		}

		[Test]
		public void StaticInvalidReadingSettingsTest()
		{
			Action act = () => SettingsManager.ReadSetting<string>("String1");

			act.Should().Throw<InvalidCastException>();
		}
	}
}

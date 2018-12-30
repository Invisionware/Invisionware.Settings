using FluentAssertions;
using Invisionware.Settings.Sinks;
using NUnit.Framework;

namespace Invisionware.Settings.Tests
{
	[TestFixture]
	[Category("Settings")]
	public class SettingsTests
	{
		[Test]
		public void OverrideTest()
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


			var settings = settingsMgr.ReadSettings<CustomSettings>();

			settings.String1.Should().Be("Override1");
		}
	}
}

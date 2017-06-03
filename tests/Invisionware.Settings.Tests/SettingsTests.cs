using FluentAssertions;
using Invisionware.Settings.Sinks;
using Microsoft.Azure.Documents.Client;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

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

			var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.Memory(defaultSettings)
			    .ReadFrom.Memory(defaultSettings)
			    .ReaderOverrides.WithAction(
				    (s) =>
				    {
					    s.String1 = "Override1";
				    })
			    .WriterOverrides.WithAction((s) =>
			    {

			    });

		    var settingsMgr = settingsConfig.CreateSettingsMgr();

		    var settings = settingsMgr.LoadSettings();

		    settings.String1.Should().Be("Override1");
	    }
	}
}

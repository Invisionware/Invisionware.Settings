# Invisionware Framework
Invisionware Frmaework is a collection of utilities classes, extension methods, and new functionality to simplify creatig application in .NET. Amost all of the libraries are support on Dektop and Mobile (including Xamarin) development environments to provide the maxinum value possible.

## Settings
This portion of the Invisionware Framework provides a Settings Framework that is very similiar to Serilog in that it supports extensions for where the settings are stored, how they are accessed, etc.

[![NuGet](https://img.shields.io/nuget/v/Invisionware.Settings.svg)](https://www.nuget.org/packages/Invisionware.Settings)

Packages related to Invisionware Settings
```powershell
Install-Package Invisionware.Settings
```

Then just add the following using statement
```c#
using Invisionware.Settings;
```

### Sinks
Sinks provide a way to read/write settings from different sources

#### AzureDocumentDb
Read/Write settings from Azure DocumentDb

#### AzureStorage
Read/Write Settings from Azure Storage

#### JsonNet
Read/Write Settings from JsonNet files

### Overrides
Overrides allow for "overriding" settings during the read/write operation.  These are useful for loading settings from one source and then updating specific individual settings from something like the CommandLine 

#### SettingsAcionOverride
A generic override that executes a specific action immediately before or after reading the settings and before returning the settings object

### Example

```c#
public class CustomSettings
{
	public string String1 { get; set; } = "String1";
	public int Int1 { get; set; } = 1;
	public CustomSettingsNested Nested { get; set; } = new CustomSettingsNested();
}

public class CustomSettingsNested
{
	public string String2 { get; set; } = "Nested.String2";
}

var f = System.IO.Path.Combine(TestContext.CurrentContext.WorkDirectory, "customSettings.json");

var settingsConfig = new SettingsConfiguration<CustomSettings>().WriteTo.JsonNet(f).ReadFrom.JsonNet(f);

var settingsMgr = settingsConfig.CreateSettingsMgr();

settings.String1 = "Test 1";
settingsMgr.SaveSettings(settings);

var settings = settingsMgr.LoadSettings();

Console.WriteLine(settings.String1);
// Test 1;
```

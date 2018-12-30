using System;
using Invisionware.Settings.EventArgs;
using Microsoft.Extensions.Configuration;

namespace Invisionware.Settings.Sinks.ConfigBuilder
{
	public class ConfigBuilderSink : ISettingsObjectReaderSink
	{
		public IConfigurationBuilder _configurationBuilder;
		private IConfigurationRoot _configuration;

		public EventHandler<SettingsLoadingEventArgs> OnSettingsRead { get; set; }
		public EventHandler<SettingsSavingEventArgs> OnSettingsWritting { get; set; }

		public ConfigBuilderSink()
		{
			_configurationBuilder = new ConfigurationBuilder();

			// Lets add the default files
			AddFile("appsettings.json", optional: true, fileType: ConfigurationFileTypes.Json);
			AddFile("appsettings.json.config", optional: true, fileType: ConfigurationFileTypes.Json);
			AddFile("appsettings.xnml.config", optional: true, fileType: ConfigurationFileTypes.Xml);

		}

		public ConfigBuilderSink(IConfigurationBuilder configurationBuilder)
		{
			_configurationBuilder = configurationBuilder ?? throw new ArgumentNullException(nameof(configurationBuilder));
		}

		public ConfigBuilderSink(string configurationFile, bool optional = false, bool reloadOnChange = false, ConfigurationFileTypes fileType = ConfigurationFileTypes.Json)
		{
			if (string.IsNullOrEmpty(configurationFile)) throw new ArgumentNullException(nameof(configurationFile));

			AddFile(configurationFile, optional, reloadOnChange, fileType);

		}

		public ConfigBuilderSink AddFile(string configurationFile, bool optional = false, bool reloadOnChange = false, ConfigurationFileTypes fileType = ConfigurationFileTypes.Json)
		{
			if (string.IsNullOrEmpty(configurationFile)) throw new ArgumentNullException(nameof(configurationFile));

			switch (fileType)
			{
				case ConfigurationFileTypes.Json:
					_configurationBuilder.AddJsonFile(configurationFile, optional, reloadOnChange);
					break;
				case ConfigurationFileTypes.Xml:
					_configurationBuilder.AddXmlFile(configurationFile, optional, reloadOnChange);
					break;
			}
			
			return this;
		}

		public bool Close()
		{
			_configuration = null;

			return true;
		}

		public bool Flush()
		{
			return true;
		}

		public bool Open()
		{
			_configuration = _configurationBuilder.Build();

			return true;
		}

		public T ReadSetting<T>() where T : class
		{
			var result = _configuration.Get<T>();

			return result;
		}
	}

	public enum ConfigurationFileTypes
	{
		Json,
		Xml
	}
}

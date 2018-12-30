using System;
using Invisionware.Settings.EventArgs;

namespace Invisionware.Settings.Sinks.AppConfig
{
    public class AppConfigSink : ISettingsReaderSink, ISettingsWriterSink
    {
        public EventHandler<SettingsLoadingEventArgs> OnSettingsRead { get; set; }
        public EventHandler<SettingsSavingEventArgs> OnSettingsWritting { get; set; }

        public bool Close()
        {
            return true;
        }

        public bool Flush()
        {
            return true;
        }

        public T Load<T>() where T : class
        {
            return default(T);
        }

        public bool Open()
        {
            System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            return true;
        }

        public bool Save<T>(T settings) where T : class
        {
            return true;
        }
    }
}

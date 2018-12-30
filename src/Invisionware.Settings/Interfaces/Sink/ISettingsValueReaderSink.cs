namespace Invisionware.Settings
{
	public interface ISettingsValueReaderSink : ISettingsReaderSink
	{
		T ReadSetting<T>(string key, T defaultValue = default(T));
	}
}
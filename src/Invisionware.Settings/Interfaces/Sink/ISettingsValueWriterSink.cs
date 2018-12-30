namespace Invisionware.Settings
{
	public interface ISettingsValueWriterSink : ISettingsWriterSink
	{
		bool WriteSetting<T>(string key, T value);

	}
}
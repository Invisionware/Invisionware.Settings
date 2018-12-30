namespace Invisionware.Settings
{
	public interface ISettingsObjectReaderSink : ISettingsReaderSink
	{
		T ReadSetting<T>() where T : class;
	}
}
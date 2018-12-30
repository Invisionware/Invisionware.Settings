namespace Invisionware.Settings
{
	public interface ISettingsObjectWriterSink : ISettingsWriterSink
	{
		bool WriteSetting<T>(T settings) where T : class;
	}
}
namespace Invisionware.Settings
{
	public interface ISettingsSink
	{
		bool Open();
		bool Flush();
		bool Close();
	}
}
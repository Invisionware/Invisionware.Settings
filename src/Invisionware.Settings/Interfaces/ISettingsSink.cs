using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsSink
	{
		bool Open();
		bool Flush();
		bool Close();
	}

	public interface ISettingsSinkAsync
	{
		Task<bool> OpenAsync();
		Task<bool> FlushAsync();
		Task<bool> CloseAsync();
	}
}
using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsSinkAsync
	{
		Task<bool> OpenAsync();
		Task<bool> FlushAsync();
		Task<bool> CloseAsync();
	}
}
using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsObjectReaderSinkAsync : ISettingsReaderSinkAsync, ISettingsSinkAsync
	{
		Task<T> ReadSettingAsync<T>() where T : class;
	}
}
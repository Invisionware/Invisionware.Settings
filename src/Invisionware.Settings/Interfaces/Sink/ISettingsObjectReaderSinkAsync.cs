using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsObjectReaderSinkAsync : ISettingsReaderSink, ISettingsSinkAsync
	{
		Task<T> ReadSettingAsync<T>() where T : class;
	}
}
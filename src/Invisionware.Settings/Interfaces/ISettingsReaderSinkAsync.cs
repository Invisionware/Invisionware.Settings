using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsReaderSinkAsync : ISettingsReaderSink, ISettingsSinkAsync
	{
		Task<T> LoadAsync<T>() where T : class;
	}
}
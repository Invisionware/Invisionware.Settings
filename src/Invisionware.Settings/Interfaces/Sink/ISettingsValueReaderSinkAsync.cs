using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsValueReaderSinkAsync : ISettingsReaderSink, ISettingsSinkAsync
	{
		Task<T> ReadSettingAsync<T>(string key, T defaultValue = default(T));
	}

}
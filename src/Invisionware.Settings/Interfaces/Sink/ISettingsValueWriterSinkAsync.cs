using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsValueWriterSinkAsync : ISettingsWriterSinkAsync, ISettingsSinkAsync
	{
		Task<bool> WriteSettingAsync<T>(string key, T value);
	}
}
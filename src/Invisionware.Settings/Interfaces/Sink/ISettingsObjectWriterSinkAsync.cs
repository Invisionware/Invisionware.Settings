using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsObjectWriterSinkAsync : ISettingsWriterSink, ISettingsSinkAsync
	{
		Task<bool> WriteSettingAsync<T>(T settings) where T : class;
	}
}
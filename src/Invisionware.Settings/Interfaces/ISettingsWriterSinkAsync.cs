using System.Threading.Tasks;

namespace Invisionware.Settings
{
	public interface ISettingsWriterSinkAsync : ISettingsWriterSink, ISettingsSinkAsync
	{
		Task<bool> SaveAsync<T>(T settings) where T : class;
	}
}
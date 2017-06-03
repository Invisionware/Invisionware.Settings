using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invisionware.Settings.EventArgs
{
	public class SettingsEventArgs : System.EventArgs
	{
		public object Data { get; set; }
	}

	public class SettingsLoadingEventArgs : SettingsEventArgs
	{
	}

	public class SettingsSavingEventArgs : SettingsEventArgs
	{
		
	}
}

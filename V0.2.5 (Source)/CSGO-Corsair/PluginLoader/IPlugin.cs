using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Corsair.PluginLoader{
	public interface IPlugin{
		/// <summary>
		/// Plugin name
		/// </summary>
		string Name {get;}

		/// <summary>
		/// Plugin host instance, set by the host after plugin has been started
		/// </summary>
		IPluginHost Host {get; set;}

		/// <summary>
		/// List of modes the plugin has, must have atleast one mode.
		/// </summary>
		Modes[] ModeList {get;}
	}
}

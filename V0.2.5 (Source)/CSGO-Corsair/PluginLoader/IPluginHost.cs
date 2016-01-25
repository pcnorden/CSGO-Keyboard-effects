using CUE.NET.Devices.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Corsair.PluginLoader{
	public interface IPluginHost{
		/// <summary>
		/// Gets the data that CS:GO sends us parsed to a dynamic.
		/// </summary>
		dynamic publicData {get;}

		/// <summary>
		/// Gets the keyboard
		/// </summary>
		CorsairKeyboard keyboard {get;}
	}
}

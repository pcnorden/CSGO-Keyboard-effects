using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Corsair.PluginLoader{
	public class Modes{
		/// <summary>
		/// Delegate for calling the method
		/// </summary>
		public delegate void updatePlugin();

		/// <summary>
		/// Name of the plugin
		/// </summary>
		public string ModeName {get; private set;}
		
		public updatePlugin update {get; private set;}

		public Modes(string ModeName, updatePlugin update){
			this.ModeName = ModeName;
			this.update = update;
		}
	}
}

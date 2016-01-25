using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Corsair.SteelSeries{
	class SteelSeriesEnum{

		#region Links
		/*
		This is a bit hacky code, but due to me needing to get string from this enum, I will create a private enum,
		that will later be used in a dictionary, so I can read string that way
		*/
		public enum link_enum{
			metadata,
			eventregister
		}
		/// <summary>
		/// <para>How to use this dictionary:</para>
		/// <para>SteelSeries.SteelSeriesEnum.uris[SteelSeries.SteelSeriesEnum.link_enum.<value>]; to get string</para>
		/// </summary>
		public static Dictionary<link_enum, string> uris = new Dictionary<link_enum, string>{
			{link_enum.metadata, "game_metadata"},
			{link_enum.eventregister, "register_event"}
		};
		#endregion
	}
}

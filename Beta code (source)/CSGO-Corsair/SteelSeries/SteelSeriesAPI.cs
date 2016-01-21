using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Corsair{
	class SteelSeriesAPI{
		#region Send META Data
		/// <summary>
		/// <para>This function is for sending the very first data</para>
		/// <para>to steelseries engine</para>
		/// <para>Only call this once! (on startup)</para>
		/// </summary>
		/// <param name="game">The internal name for the app</param>
		/// <param name="display_name">The program the engine will show</param>
		/// <param name="port">The port the engine listens on</param>
		public static void sendMetaData(string game, string display_name, int port){
			dynamic data = new ExpandoObject();
			data.game = game;
			data.game_display_name = display_name;
			data.icon_color_id = 11;
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
			sendPostData(SteelSeries.SteelSeriesEnum.uris[SteelSeries.SteelSeriesEnum.link_enum.metadata], json, port);
		}
		#endregion

		#region Send POST data
		/// <summary>
		/// <para>To send data to the engine, we need to post it to local ip addr</para>
		/// <para></para>
		/// </summary>
		/// <param name="uri">This is the place where you put the subaddr from the enum steelseries.subaddr</param>
		/// <param name="data">What data to send (NEEDS TO BE JSON!!!)</param>
		/// <param name="port">What port the gamesense server is listening to</param>
		private static void sendPostData(string uri, string data, int port){
			WebRequest request = WebRequest.Create("http://127.0.0.1:"+port+"/"+uri);
			request.Method = "POST";
			byte[] byteArray = Encoding.UTF8.GetBytes(data);
			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;
			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();
			WebResponse response = request.GetResponse();
			Console.WriteLine(((HttpWebResponse)response).StatusCode);
			response.Close();
		}
		#endregion

		#region Register Events
		/// <summary>
		/// 
		/// </summary>
		/// <param name="game">The internal name for the app</param>
		/// <param name="events">What event to register</param>
		/// <param name="port"></param>
		public static void registerEvents(string game, string events, int port){

		}
		#endregion
	}
}
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
			sendPostData("game_metadata", json, port);
		}
		/// <summary>
		/// <para>To send data to the engine, we need to post it to local ip addr</para>
		/// <para></para>
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="data"></param>
		/// <param name="port"></param>
		public static void sendPostData(string uri, string data, int port){
			WebRequest request = WebRequest.Create("http://127.0.0.1:"+port+"/"+uri);
			request.Method = "POST";
			byte[] byteArray = Encoding.UTF8.GetBytes(data);
			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;
			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();
		}
	}
}
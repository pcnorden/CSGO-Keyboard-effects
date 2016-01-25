using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace CSGO_Corsair {
	class WebServer{

		private static HttpListener listener;
		private static Thread thread;

		public static void initServer(){
			listener = new HttpListener();
			listener.Prefixes.Add("http://127.0.0.1:3000/");
			listener.Prefixes.Add("http://localhost:3000/");
			listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

			listener.Start();
			thread = new Thread(new ParameterizedThreadStart(startlistener));
			thread.Start();
		}
		private static void startlistener(object s){
			while(true){
				processRequest();
			}
		}
		private static void processRequest(){
			var result = listener.BeginGetContext(ListenerCallback, listener);
			result.AsyncWaitHandle.WaitOne();
		}
		private static void ListenerCallback(IAsyncResult result){
			var context = listener.EndGetContext(result);
			var data_text = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding).ReadToEnd();
			var cleaned_data = System.Web.HttpUtility.UrlDecode(data_text);
			context.Response.StatusCode = 200;
			context.Response.StatusDescription = "OK";
			dynamic m = JsonConvert.DeserializeObject(cleaned_data);

			int currentWeapon = DecodeJson.current_weapon(m);

			DecodeJson.paint_ammo(currentWeapon, DecodeJson.get_ammo(m, currentWeapon), DecodeJson.get_max_ammo(m, currentWeapon), m);
			context.Response.Close();
		}
	}
}

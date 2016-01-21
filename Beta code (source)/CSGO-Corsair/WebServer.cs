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
		public static void ListenerCallback(IAsyncResult result){
			var context = listener.EndGetContext(result);
			var data_text = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding).ReadToEnd();
			var cleaned_data = System.Web.HttpUtility.UrlDecode(data_text);
			context.Response.StatusCode = 200;
			context.Response.StatusDescription = "OK";
			dynamic m = JsonConvert.DeserializeObject(cleaned_data);

			DecodeJson decode = new DecodeJson();
			decode.paint_keys(m); // Loads and runs the non-static class

			context.Response.Close();
		}
	}
}

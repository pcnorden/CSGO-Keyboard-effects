using CUE.NET.Devices.Keyboard.Enums;
using System;
using System.Drawing;
using System.IO;
using System.Xml;

/*
The MIT License (MIT)

Copyright (c) 2016 pcnorden

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software(*), and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

[*] Due to me being the person I am, I say that you are NOT allowed to sell licenses, as I intend to keep this free of charge.
*/

namespace CSGO_Corsair {
	class Program{

		public static readonly string configName = "csgo_keyboard_config.xml";

		public static CorsairKeyboardKeyId[] keys = new CorsairKeyboardKeyId[7];
		public static string[] steelSeriesKeys = new string[7];

		private static readonly string[] notSupported = {"This functionality is gonna be implemented soon",
			"Due to me not knowing anyone that uses",
			"a steelseries/razer keyboard, I can't test"};

		static void Main(string[] args){
			Console.ForegroundColor = ConsoleColor.Yellow;

			#region Create default XML file if not existing
			if(!File.Exists(configName)){
				Console.WriteLine("csgo_keyboard_config.xml doesn't exists, creating default template");
				if(templatexml.createTemplateConfig(false, configName) == false){
					errorWrite("Failed to create a template file!\nSomething is really wrong! Please open an issue on github!\n\nPress any key to exit");
					Console.ReadKey();
					Environment.Exit(0);
				}
			}
			#endregion

			#region Read config from xml file
			XmlDocument xml = new XmlDocument();
			xml.Load(configName);
			XmlNode consoleTitleNode = xml.DocumentElement.SelectSingleNode("/root/ConsoleTitle");

			keys = customxmlfunctions.ReadCustomKeys.readAmmoKeysCorsair(configName);
			#endregion

			Console.WriteLine("Loaded XmlNodes");

			#region Title
			try{
				Console.Title = consoleTitleNode.InnerText+" | Made by /u/pcnorden on reddit";
			}catch(Exception){
				Console.Title = "ERROR IN XML FILE | Made by /u/pcnorden on reddit";
			}
			#endregion

			customxmlfunctions.ReadCustomKeys.readCustomKeysFromFile(configName);

			Console.WriteLine("Custom title: "+Console.Title);
			Console.ForegroundColor = ConsoleColor.White;
			
			// We will try and launch the corsair effects engine SDK wrapper, and if returned true, we have suceeded
			if(CorsairKeyboardAPI.initSDK()){
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Initialized the corsair wrapper");
				WebServer.initServer();
				CorsairKeyboardAPI.colorCustomKeys();
				Console.WriteLine("Started the server\n\nPress escape when this window is active to close the program");
				console.userConsole.startConsole();
			}else{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\"I'm sorry Dave, I'm afraid I can't do that\"");
				Console.WriteLine("Failed to initialize the Corsair SDK, please check that you have CUE running");
				Console.WriteLine("Press any key to exit");
				Console.ReadKey();
				Environment.Exit(0);
			}
		}
		#region Create HSV instead of RGB
		public static Color ColorFromHSV(double hue, double saturation, double value){
			int hi = Convert.ToInt32(Math.Floor(hue/60))%6;
			double f = hue/60-Math.Floor(hue/60);
			value = value*255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value*(1-saturation));
			int q = Convert.ToInt32(value*(1-f*saturation));
			int t = Convert.ToInt32(value*(1-(1-f)*saturation));
			if(hi == 0){
				return Color.FromArgb(255, v, t, p);
			}else if(hi == 1){
				return Color.FromArgb(255, q, v, p);
			}else if(hi == 2){
				return Color.FromArgb(255, p, v, t);
			}else if(hi == 3){
				return Color.FromArgb(255, t, p, v);
			}else if(hi == 4){
				return Color.FromArgb(255, t, p, v);
			}else{
				return Color.FromArgb(255, v, p, q);
			}
		}
		#endregion

		public static void errorWrite(string msg){
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Error: "+msg);
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
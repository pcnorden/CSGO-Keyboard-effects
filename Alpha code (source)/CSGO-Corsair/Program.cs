using CUE.NET.Devices.Keyboard.Enums;
using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;

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

		public static string configName = "csgo_keyboard_config.xml";
		private static bool useCorsair;

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
				new XDocument(
					new XElement("root",
						new XElement("weapon_0_button_corsair",Convert.ChangeType(CorsairKeyboardKeyId.D1, typeof(string))),
						new XElement("weapon_1_button_corsair",Convert.ChangeType(CorsairKeyboardKeyId.D2, typeof(string))),
						new XElement("weapon_2_button_corsair",Convert.ChangeType(CorsairKeyboardKeyId.D3, typeof(string))),
						new XElement("weapon_3_button_corsair",Convert.ChangeType(CorsairKeyboardKeyId.D4, typeof(string))),
						new XElement("weapon_4_button_corsair",Convert.ChangeType(CorsairKeyboardKeyId.D5, typeof(string))),
						new XElement("weapon_5_button_corsair",Convert.ChangeType(CorsairKeyboardKeyId.D6, typeof(string))),
						new XElement("weapon_6_button_corsair",Convert.ChangeType(CorsairKeyboardKeyId.D7, typeof(string))),
						//https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/standard-zones.md#zones-specifying-various-individual-keys
						new XElement("weapon_0_button_steelseries","keyboard-1"),
						new XElement("weapon_1_button_steelseries","keyboard-2"),
						new XElement("weapon_2_button_steelseries","keyboard-3"),
						new XElement("weapon_3_button_steelseries","keyboard-4"),
						new XElement("weapon_4_button_steelseries","keyboard-5"),
						new XElement("weapon_5_button_steelseries","keyboard-6"),
						new XElement("weapon_6_button_steelseries","keyboard-7"),
						new XElement("ConsoleTitle","CS:GO Keyboard Effetcs | APLHA"),
						new XElement("Engine","Corsair")
						)
					).Save(configName);
			}
			#endregion

			#region Read config from xml file
			XmlDocument xml = new XmlDocument();
			xml.Load(configName);
			XmlNode consoleTitleNode = xml.DocumentElement.SelectSingleNode("/root/ConsoleTitle");
			XmlNode weapon_0_corsair = xml.DocumentElement.SelectSingleNode("/root/weapon_0_button_corsair");
			XmlNode weapon_1_corsair = xml.DocumentElement.SelectSingleNode("/root/weapon_1_button_corsair");
			XmlNode weapon_2_corsair = xml.DocumentElement.SelectSingleNode("/root/weapon_2_button_corsair");
			XmlNode weapon_3_corsair = xml.DocumentElement.SelectSingleNode("/root/weapon_3_button_corsair");
			XmlNode weapon_4_corsair = xml.DocumentElement.SelectSingleNode("/root/weapon_4_button_corsair");
			XmlNode weapon_5_corsair = xml.DocumentElement.SelectSingleNode("/root/weapon_5_button_corsair");
			XmlNode weapon_6_corsair = xml.DocumentElement.SelectSingleNode("/root/weapon_6_button_corsair");

			XmlNode weapon_0_steelseries = xml.DocumentElement.SelectSingleNode("/root/weapon_0_button_steelseries");
			XmlNode weapon_1_steelseries = xml.DocumentElement.SelectSingleNode("/root/weapon_1_button_steelseries");
			XmlNode weapon_2_steelseries = xml.DocumentElement.SelectSingleNode("/root/weapon_2_button_steelseries");
			XmlNode weapon_3_steelseries = xml.DocumentElement.SelectSingleNode("/root/weapon_3_button_steelseries");
			XmlNode weapon_4_steelseries = xml.DocumentElement.SelectSingleNode("/root/weapon_4_button_steelseries");
			XmlNode weapon_5_steelseries = xml.DocumentElement.SelectSingleNode("/root/weapon_5_button_steelseries");
			XmlNode weapon_6_steelseries = xml.DocumentElement.SelectSingleNode("/root/weapon_6_button_steelseries");

			XmlNode keyboard_engine = xml.DocumentElement.SelectSingleNode("/root/Engine");
			#endregion
			Console.WriteLine("Loaded XmlNodes");

			#region Get engine from config file
			try{
				if(keyboard_engine.InnerText.ToLower() == "corsair"){
					useCorsair = true;
				}else if(keyboard_engine.InnerText.ToLower() == "steelseries"){
					useCorsair = true; // Will implement steelseries keyboards later.
				}
			}catch(Exception e){
				errorWrite("NO ENGINE SELECTED!\nPlease choose between \"Corsair\" or \"steelseries\"\nPress any key to exit!\nError msg: "+e.Message);
				Console.ReadKey();
				Environment.Exit(0);
			}
			#endregion
			Console.WriteLine("Got what engine to use");

			#region Title
			try {
				Console.Title = consoleTitleNode.InnerText+" | Made by /u/pcnorden on reddit";
			}catch(Exception){
				Console.Title = "ERROR IN XML FILE | Made by /u/pcnorden on reddit";
			}
			#endregion
			Console.WriteLine("Custom title: "+Console.Title);

			#region Get weapon_0 key from config file
			try{
				if(useCorsair)
					keys[0] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), weapon_0_corsair.InnerText);
				else
					steelSeriesKeys[0] = weapon_0_steelseries.InnerText;
			}catch(Exception){
				errorWrite("Cound not find key id in config file for weapon_0! Reverting to default key!");
				if(useCorsair)
					keys[0] = CorsairKeyboardKeyId.D1;
				else
					steelSeriesKeys[0] = "keyboard-1";
			}
			#endregion
			Console.WriteLine("Loaded weapon_0 key");

			#region Get weapon_1 key from config file
			try{
				if(useCorsair)
					keys[1] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), weapon_1_corsair.InnerText);
				else
					steelSeriesKeys[1] = weapon_1_steelseries.InnerText;
			}catch(Exception){
				errorWrite("Could not find key id in config file for weapon_1! Reverting to default key!");
				if(useCorsair)
					keys[1] = CorsairKeyboardKeyId.D2;
				else
					steelSeriesKeys[1] = "keyboard-1";
			}
			#endregion
			Console.WriteLine("Loaded weapon_1 key");

			#region Get weapon_2 key from config file
			try{
				if(useCorsair)
					keys[2] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), weapon_2_corsair.InnerText);
				else
					steelSeriesKeys[2] = weapon_2_steelseries.InnerText;
			}catch(Exception){
				errorWrite("Could not find key id in config file for weapon_2! Reverting to default key!");
				if(useCorsair)
					keys[2] = CorsairKeyboardKeyId.D3;
				else
					steelSeriesKeys[2] = "keyboard-3";
			}
			#endregion
			Console.WriteLine("Loaded weapon_2 key");

			#region Get weapon_3 key from config file
			try{
				if(useCorsair)
					keys[3] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), weapon_3_corsair.InnerText);
				else
					steelSeriesKeys[3] = weapon_3_steelseries.InnerText;
			}catch(Exception){
				errorWrite("Could not find key id in config file for weapon_3! Reverting to defualt key!");
				if(useCorsair)
					keys[3] = CorsairKeyboardKeyId.D4;
				else
					steelSeriesKeys[3] = "keyboard-4";
			}
			#endregion
			Console.WriteLine("Loaded weapon_3 key");

			#region Get weapon_4 key from config file
			try{
				if(useCorsair)
					keys[4] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), weapon_4_corsair.InnerText);
				else
					steelSeriesKeys[4] = weapon_4_steelseries.InnerText;
			}catch(Exception){
				errorWrite("Could not find key id in config file for weapon_4! Reverting to default key!");
				if(useCorsair)
					keys[4] = CorsairKeyboardKeyId.D5;
				else
					steelSeriesKeys[4] = "keyboard-5";
			}
			#endregion
			Console.WriteLine("Loaded weapon_4 key");

			#region Get weapon_5 key from config file
			try{
				if(useCorsair)
					keys[5] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), weapon_5_corsair.InnerText);
				else
					steelSeriesKeys[5] = weapon_5_steelseries.InnerText;
			}catch(Exception){
				errorWrite("Could not find key id in config file for weapon_5! Reverting to default key!");
				if(useCorsair)
					keys[5] = CorsairKeyboardKeyId.D6;
				else
					steelSeriesKeys[5] = "keyboard-6";
			}
			#endregion
			Console.WriteLine("Loaded weapon_5 key");

			#region Get weapon_6 key from config file
			try{
				if(useCorsair)
					keys[6] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), weapon_6_corsair.InnerText);
				else
					steelSeriesKeys[6] = weapon_6_steelseries.InnerText;
			}catch(Exception){
				errorWrite("Could not find key id in config file for weapon_6! Reverting to default key!");
				if(useCorsair)
					keys[6] = CorsairKeyboardKeyId.D7;
				else
					steelSeriesKeys[6] = "keyboard-7";
			}
			#endregion
			Console.WriteLine("Loaded weapon_6 key\n");
			Console.ForegroundColor = ConsoleColor.White;
			
			if(useCorsair){
				// We will try and launch the corsair effects engine SDK wrapper, and if returned true, we have suceeded
				if(CorsairKeyboardAPI.initSDK()){
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Initialized the corsair wrapper");
					WebServer.initServer();
					Console.WriteLine("Started the server\n\nPress escape when this window is active to close the program");
					while(true){
						ConsoleKeyInfo key = Console.ReadKey();
						if(key.Key == ConsoleKey.Escape){
							Environment.Exit(0);
						}
					}
				}else{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\"I'm sorry Dave, I'm afraid I can't do that\"");
					Console.WriteLine("Failed to initialize the Corsair SDK, please check that you have CUE running");
					Console.WriteLine("Press any key to exit");
					Console.ReadKey();
					Environment.Exit(0);
				}
			}else{
				Console.ForegroundColor = ConsoleColor.Cyan;
				foreach(string str in notSupported)
					Console.WriteLine(str);
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
using CUE.NET.Devices.Keyboard.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace CSGO_Corsair.customxmlfunctions {
	class ReadCustomKeys{

		public static List<Color> customColors = new List<Color>();
		public static List<CorsairKeyboardKeyId> customKeys = new List<CorsairKeyboardKeyId>();

		public static void readCustomKeysFromFile(string filename){
			customColors.Clear();
			customKeys.Clear();
			XmlDocument xml = new XmlDocument();
			xml.Load(filename);
			XmlNode keys = xml.DocumentElement.SelectSingleNode("/root/customCorsairKeys");
			try{
				foreach(XmlNode key in keys.ChildNodes){
					customColors.Add(ColorTranslator.FromHtml(key.Attributes["color"].Value));
					customKeys.Add((CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), key.InnerText));
					Console.WriteLine("key: "+key.InnerText+" attribute: "+ColorTranslator.FromHtml(key.Attributes["color"].Value));
				}
			}catch(Exception e){
				Program.errorWrite(e.Message);
			}
		}

		/// <summary>
		/// <para>Opens and reads the configuration file, and returns the array</para>
		/// <para>with CorsairKeyboardKeyId:s</para>
		/// </summary>
		/// <param name="filename">the config file filename</param>
		/// <returns>Returns the ammunation keys</returns>
		public static CorsairKeyboardKeyId[] readAmmoKeysCorsair(string filename){
			CorsairKeyboardKeyId[] defaultKeys = {CorsairKeyboardKeyId.D1, CorsairKeyboardKeyId.D2,
			CorsairKeyboardKeyId.D3, CorsairKeyboardKeyId.D4, CorsairKeyboardKeyId.D5,
			CorsairKeyboardKeyId.D6, CorsairKeyboardKeyId.D7}; // If we couldn't read a key, replace it with the default

			XmlDocument doc = new XmlDocument(); // New XmlDocument so we can read the config file
			doc.Load(filename); // Load the xml file, with the string from the main program

			List<XmlNode> list = new List<XmlNode>(); // Create a list we can store the xml nodes in

			#region Create all the XML nodes!
			XmlNode weapon_node0 = doc.DocumentElement.SelectSingleNode("/root/ammunitionKeysCorsair/weapon_0");
			XmlNode weapon_node1 = doc.DocumentElement.SelectSingleNode("/root/ammunitionKeysCorsair/weapon_1");
			XmlNode weapon_node2 = doc.DocumentElement.SelectSingleNode("/root/ammunitionKeysCorsair/weapon_2");
			XmlNode weapon_node3 = doc.DocumentElement.SelectSingleNode("/root/ammunitionKeysCorsair/weapon_3");
			XmlNode weapon_node4 = doc.DocumentElement.SelectSingleNode("/root/ammunitionKeysCorsair/weapon_4");
			XmlNode weapon_node5 = doc.DocumentElement.SelectSingleNode("/root/ammunitionKeysCorsair/weapon_5");
			XmlNode weapon_node6 = doc.DocumentElement.SelectSingleNode("/root/ammunitionKeysCorsair/weapon_6");
			#endregion

			List<XmlNode> nodeList = new List<XmlNode>(); // To make the searching simple, add the nodes to a list to enumerate thru
			nodeList.Add(weapon_node0); // BEGIN THE TRANSACTION!
			nodeList.Add(weapon_node1);
			nodeList.Add(weapon_node2);
			nodeList.Add(weapon_node3); // Dum di dum... How's your day?
			nodeList.Add(weapon_node4);
			nodeList.Add(weapon_node5);
			nodeList.Add(weapon_node6); // END THE TRANSACTON!

			CorsairKeyboardKeyId[] keyList = new CorsairKeyboardKeyId[7]; // Create an array matching what we can display
			for(int i=0; i<7; i++){ // To enumerate througt the nodes
				try{
					keyList[i] = (CorsairKeyboardKeyId)Enum.Parse(typeof(CorsairKeyboardKeyId), nodeList[i].InnerText); // insert custom key into the array/list, converted to a CorsairKeyboardKeyId
					Console.WriteLine(i+" : "+keyList[i]);
				}catch(Exception){ // If we fail to load, this will be triggered. Also if something else goes wrong... but is shouldn't happen.
					Program.errorWrite("Could not load a value from \"weapon_"+i+"button_corsair! Reverting to default key!"); // Notifying the user of the xml error.
					if(i >= defaultKeys.Length) //Grabs the last default key if list lenght is more than defaultKeys array lenght.
						keyList[i] = defaultKeys[defaultKeys.Length-1]; // If I am right, this will get the last default key.
					else// Grabs the default key if list lenght is less then defaultKeys array lenght.
						keyList[i] = defaultKeys[i]; // Grabs the key.
				}
			}
			return keyList; // Since we arn't a void method, we need to return the array.
		}
	}
}

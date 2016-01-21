using CUE.NET.Devices.Keyboard.Enums;
using System;
using System.IO;
using System.Xml.Linq;
using System.Drawing;

namespace CSGO_Corsair{
	class templatexml{
		#region createTemplateConfig(bool overwrite, string filename)
		/// <summary>
		/// <para>This function is to create the "normal" configuration file</para>
		/// <para>If the file exists, and you would like to override, you must set "overwrite" parameter to true</para>
		/// </summary>
		/// <param name="overwrite">If the file exists, you must set this to true to overwrite the file</param>
		/// <param name="filename">The configuration file name</param>
		/// <returns>true if the file was created, false is something is wrong</returns>
		public static bool createTemplateConfig(bool overwrite, string filename){
			if(File.Exists(filename) && overwrite == true){
				create(filename);
				return true;
			}else if(File.Exists(filename) && overwrite == false){
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("\nCreateTemplateXml.cs error : overwrite = false");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("\n");
				return false;
			}else if(!File.Exists(filename)){
				create(filename);
				return true;
			}else
				return false;
		}
		#endregion

		#region Create XML-file with default data
		private static void create(string filename){
			XElement root = new XElement("root"); // Create the root of the tree structure

			#region Create ammunition subgroup
			XElement ammoGroup = new XElement("ammunitionKeysCorsair"); // Create the subgroup
			ammoGroup.Add(new XElement("weapon_0", Convert.ChangeType(CorsairKeyboardKeyId.D1, typeof(string)))); // Add the digit "1"-key as string into the group
			ammoGroup.Add(new XElement("weapon_1", Convert.ChangeType(CorsairKeyboardKeyId.D2, typeof(string)))); // Add the digit "2"-key as string into the group
			ammoGroup.Add(new XElement("weapon_2", Convert.ChangeType(CorsairKeyboardKeyId.D3, typeof(string)))); // Add the digit "3"-key as string into the group
			ammoGroup.Add(new XElement("weapon_3", Convert.ChangeType(CorsairKeyboardKeyId.D4, typeof(string)))); // Add the digit "4"-key as string into the group
			ammoGroup.Add(new XElement("weapon_4", Convert.ChangeType(CorsairKeyboardKeyId.D5, typeof(string)))); // Add the digit "5"-key as string into the group
			ammoGroup.Add(new XElement("weapon_5", Convert.ChangeType(CorsairKeyboardKeyId.D6, typeof(string)))); // Add the digit "6"-key as string into the group
			ammoGroup.Add(new XElement("weapon_6", Convert.ChangeType(CorsairKeyboardKeyId.D7, typeof(string)))); // Add the digit "7"-key as string into the group
			#endregion

			root.Add(ammoGroup); // Add the ammunition subgroup to the file

			#region Custom key coloring subgroup
			XElement customKeyColors = new XElement("customCorsairKeys"); // We will create a new element, where we place child nodes
			customKeyColors.Add(new XElement("key", new XAttribute("color","#ffffff"), Convert.ChangeType(CorsairKeyboardKeyId.W, typeof(string))));
			customKeyColors.Add(new XElement("key", new XAttribute("color","#ffffff"), Convert.ChangeType(CorsairKeyboardKeyId.A, typeof(string))));
			customKeyColors.Add(new XElement("key", new XAttribute("color","#ffffff"), Convert.ChangeType(CorsairKeyboardKeyId.S, typeof(string))));
			customKeyColors.Add(new XElement("key", new XAttribute("color","#ffffff"), Convert.ChangeType(CorsairKeyboardKeyId.D, typeof(string))));
			customKeyColors.Add(new XElement("key", new XAttribute("color","#ffffff"), Convert.ChangeType(CorsairKeyboardKeyId.Escape, typeof(string))));
			#endregion

			root.Add(customKeyColors); // Add the custom lighning to the file
			
			// This is all the small things, such as update and console title
			root.Add(new XElement("CheckForUpdate",false)); // To be implemented...
			root.Add(new XElement("ConsoleTitle","CS:GO Keyboard Effetcs | APLHA")); // The title of the console window.
			root.Add(new XElement("keyboardEngine","corsair")); // To be implemented... when I can afford razer and steelseries keyboards

			// Writes the XML file...
			XDocument xml = new XDocument(root);
			xml.Save(filename);
		}
		#endregion
	}
}
using System;
using System.Collections.Generic;

namespace CSGO_Corsair.console{
	class userConsole{
		/*
			A word of warning!
			This is the console main class, and this contains all the stuff the user can get to
			Do not mess around to much in this jungle, as a lot of code was pieced together
		*/
		public static void startConsole(){ // This function will basically "initialize" a set of commands for the user to use
			Console.CursorVisible = true;
			consoleTitle();
			while(true){ // Just to make sure the main thread don't close after we have entered a command
				Console.Write("> ");
				string input = Console.ReadLine().ToLower(); // Get the user input
				if(input.Contains(" ")){
					string[] buff = input.Split(' ');
					Action<string[]> test;
					bool tryget = commands.TryGetValue(buff[0], out test);
					if(tryget == true){
						if(buff[0] == "help"){
							test.Invoke(buff);
							continue;
						}else{
							List<string> list = new List<string>(buff);
							list.RemoveAt(0);
							test.Invoke(list.ToArray());
						}
					}else{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Unknown command, try \"help\" to list all commands!");
						Console.ForegroundColor = ConsoleColor.White;
					}
				}else{ // If the input didn't contain any spaces, do the gagnam style!
					string[] argument = null;
					Action<string[]> test;
					bool tryget = commands.TryGetValue(input, out test);
					if(tryget == true){
						test.Invoke(argument);
					}else if(tryget == false){
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Unkown command, try \"help\" to list all commands!");
						Console.ForegroundColor = ConsoleColor.White;
					}
				}
			}
		}
		private static void consoleTitle(){
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("CS:GO-Keyboard-Effects console v0.0.1");
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("Type \"help\" to see what commands exists");
			Console.ForegroundColor = ConsoleColor.White;
		}
		private static void reloadXML(string[] args = null){
			if(args != null && args.Length == 1 && args[0] == "-y"){ // Checks if we got the command "reloadxml -y", and if true, reload the xml file
				CorsairKeyboardAPI.clearAllKeys(); // Sets all the keys on the keyboard to black
				Program.keys = customxmlfunctions.ReadCustomKeys.readAmmoKeysCorsair(Program.configName); // Sets the ammunation keys
				customxmlfunctions.ReadCustomKeys.readCustomKeysFromFile(Program.configName); // Sets the custom keys
			}else{ // If we didn't get "reloadxml -y", ask the user if it is sure
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Are you sure you want to reload the XML file?(y/n)");
				ConsoleKeyInfo key = Console.ReadKey(true); // readkey is set to true, so we don't leave an uggly letter in the wrong spot
				if(key.Key == ConsoleKey.Y){
					CorsairKeyboardAPI.clearAllKeys(); // Sets all the keys on the keyboard to black
					Program.keys = customxmlfunctions.ReadCustomKeys.readAmmoKeysCorsair(Program.configName);
					customxmlfunctions.ReadCustomKeys.readCustomKeysFromFile(Program.configName);
					Console.ForegroundColor = ConsoleColor.White;
				}else{
					Console.WriteLine("Aborted");
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}
		private static void clearConsole(string[] args = null){ // Clears the console
			Console.ForegroundColor = ConsoleColor.White; // Sets the text color to white
			Console.Clear(); // Clears the console of all the text
			Console.SetCursorPosition(0,0); // Sets the cursor back to it's original point
			consoleTitle();
		}
		private static void help(string[] args = null){
			if(args != null && args.Length == 2){
				string test;
				bool tryget = commandHelp.TryGetValue(args[1], out test);
				if(tryget == true){
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(test);
					Console.ForegroundColor = ConsoleColor.White;
				}else if(tryget == false){
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("No help info on the command \""+args[1]+"\"!");
					Console.ForegroundColor = ConsoleColor.White;
				}
			}else{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Available commands:\n"); // Adds a "headline" to the commands part
				foreach(string commandsText in commands.Keys){
					Console.WriteLine(commandsText);
				}
			}
			Console.ForegroundColor = ConsoleColor.White;
		}
		private static void exit(string[] args = null){ // if the user would like to exit, we call this method, and ask nicely
			if(args != null && args.Length == 1 && args[0] == "-y"){ // Checks if we got a command like "exit -y", and exits if true
				Environment.Exit(0);
			}else{ // Else we ask if the user is sure
				Console.WriteLine("Are you sure you want to exit? (y/n)"); // Displays some text to the user
				ConsoleKeyInfo key = Console.ReadKey(true); // Stores the key the user presses as answer
				if(key.Key == ConsoleKey.Y){ // If the key is Y, then exit
					Environment.Exit(0);
				}else{ // If not Y, then tell the user something.
					Console.WriteLine("Other key was pressed. Not quitting!");
				}
			}
		}
		private static void createDefaultXML(string[] args){
			// TODO
			Console.WriteLine("Due to several revisions of the xml file, this has not been implemented!");
		}
		private static void loadPlugins(string[] args){
			// TODO
		}
		private static Dictionary<string, Action<string[]>> commands = new Dictionary<string, Action<string[]>>{
			{"help", help},
			{"exit", exit},
			{"clear", clearConsole},
            {"reloadxml", reloadXML},
            {"createdefaultxmlfile", createDefaultXML}
		};
		private static Dictionary<string, string> commandHelp = new Dictionary<string, string>{
			{"help","Lists all the commands you can use in this console.\nUse \"help <command>\" to see what diffrent commands do"},
			{"exit","If you would like to exit this application, use exit.\nList of arguments:\n* -y (answers yes and skips the question)"},
			{"clear","Clears the console if you think it's to cluttered with text.\n(Doesn't have any arguments)"},
			{"reloadxml","Reloads the keys and custom keys from the xml file. Use with caution, as this part doesn't have error-catching!\n(Doesn't have any arguments)"},
            {"createdefaultxmlfile","Creates a new template XML file if the old one is damaged.\nWARNING! THIS WILL OVERRIDE THE OLD FILE!\nArguments:\n* -y (answers yes and skips the override question)"}
		};
	}
}
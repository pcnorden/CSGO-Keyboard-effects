using CSGO_Corsair.PluginLoader;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Enums;
using System.Drawing;

namespace ExamplePlugin{

	public class ExamplePlugin : IPlugin{

		public string Name {get{ return "Example plugin";}} // sets the name of the plugin

		public IPluginHost Host {get; set;} // Have a host so we can access keyboard and json data

		public Modes[] ModeList {get{return modeList;}} // What funtions we have that will be called when we receive json data

		private Modes[] modeList; // Function list, but local so we can change it

		public ExamplePlugin(){
			modeList = new Modes[]{new Modes("Color accoring to team", doWork)}; // Adds a new function that the program will call when it reveieces JSON data
		}
		public void doWork(){ // The function the program will call. Needs to be public, not static as that made the program to crash
			dynamic data = Host.publicData; // Gets the parsed json data
			CorsairKeyboard kb = Host.keyboard; // Gets the keyboard so we can change leds. The keyboard updates itself, so we don't need to call keyboard.update()
			
			try{ // When you are extracting data from json data, ALWAYS surround in try/catch, or else the program will fail
				if(data.player.activity == "playing"){
					if(data.player.team == "CT")
						kb[CorsairKeyboardKeyId.Tab].Led.Color = Color.Blue; // Sets the tab key to blue
					else if(data.player.team == "T")
						kb[CorsairKeyboardKeyId.Tab].Led.Color = Color.Orange; // Sets the tab key to orange
				}else{
					kb[CorsairKeyboardKeyId.Tab].Led.Color = Color.Yellow; // Sets the tab key to yellow
				}
			}catch{}
		}
    }
}

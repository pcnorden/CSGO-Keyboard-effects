using CSGO_Corsair.PluginLoader;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Enums;
using System.Drawing;

namespace ExamplePlugin {
	public class ExamplePlugin : IPlugin{
		public string Name {get{return "Example plugin";}} // Get the visible name of the plugin

		public IPluginHost Host {get; set;} // Make so that we can have access to the data shared to us (currently data sent, and keyboard)

		public Modes[] ModeList {get{return modeList;}} // The list of functions we would like to add

		private Modes[] modeList; // Save a local copy, that other programs can't modify.

		public ExamplePlugin(){ // To add functions to the list, use the same name on namespace, class and this!
			modeList = new Modes[]{new Modes("Color accoring to team",doWork)}; // Add a function to the list
		}
		public void doWork(){ // Need's to be public, or else we can't call the function
			dynamic data = Host.publicData; // Retrieve the data CS:GO sends
			CorsairKeyboard kb = Host.keyboard; // Retrieve the keyboard, so we can color keys
			
			try{ // ALWAYS use try/catch, because if this class generates a fatal error, the whole program dies along with it
				if(data.player.activity == "playing"){ // If the player is playing
					if(data.player.team == "CT"){ // If the player is on the team counter-terrorists
						kb[CorsairKeyboardKeyId.W].Led.Color = Color.Blue; // Color the key "W" blue
						kb[CorsairKeyboardKeyId.A].Led.Color = Color.Blue;
						kb[CorsairKeyboardKeyId.S].Led.Color = Color.Blue;
						kb[CorsairKeyboardKeyId.D].Led.Color = Color.Blue;
					}else if(data.player.team == "T"){ // If the player is playing as terrorists
						kb[CorsairKeyboardKeyId.W].Led.Color = Color.Orange; // Color the key "W" orange
						kb[CorsairKeyboardKeyId.A].Led.Color = Color.Orange;
						kb[CorsairKeyboardKeyId.S].Led.Color = Color.Orange;
						kb[CorsairKeyboardKeyId.D].Led.Color = Color.Orange;
					}
				}else{ // If the player is in menu or something else, color "WASD" lime-green.
					kb[CorsairKeyboardKeyId.W].Led.Color = Color.Lime;
					kb[CorsairKeyboardKeyId.A].Led.Color = Color.Lime;
					kb[CorsairKeyboardKeyId.S].Led.Color = Color.Lime;
					kb[CorsairKeyboardKeyId.D].Led.Color = Color.Lime;
				}
			}catch{} // If there was an error extracting the data, just ignore it. We don't need to return anything
		}
	}
}

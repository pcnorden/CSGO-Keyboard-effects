using System;
using System.Drawing;
using CUE.NET;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Enums;
using CUE.NET.Exceptions;
using CUE.NET.Devices.Keyboard.Keys;

namespace CSGO_Corsair {
	class CorsairKeyboardAPI{
		#region function setKeyColor
		/// <summary>
		/// This function is used AFTER the keyboard SDK have been started!
		/// </summary>
		/// <param name="color">The color(System.Drawing.Color) you want</param>
		/// <param name="key">The CorsairKeyboardId key</param>
		/// <param name="showError">If false, then the program won't write the error code. Defaults to true</param>
		public static void setKeyColor(Color color, CorsairKeyboardKeyId key){
			//Console.WriteLine(color.ToString()+" : "+key);
			try{
				keyboard[key].Led.Color = color;
			}catch(Exception e){
				Program.errorWrite("Failed to update key, error msg: "+e.Message);
			}
		}
		#endregion

		#region setKeyColors
		/// <summary>
		/// To double productivity, we give this function an ARRAY of CorsairKeyboardKeyId's.
		/// </summary>
		/// <param name="color">What color the keys should be</param>
		/// <param name="keys">The CorsairKeyboardKeyId array to be painted</param>
		public static void setKeyColors(Color color, CorsairKeyboardKeyId[] keys){
			try{
				foreach(CorsairKeyboardKeyId tempKey in keys){
					keyboard[tempKey].Led.Color = color;
				}
			}catch(Exception e){
				Program.errorWrite("Failed to update keyGroup, error msg: "+e.Message);
			}
		}
		#endregion

		#region colorCustomKeys
		public static void colorCustomKeys(){
			try{
				int i=0;
				foreach(CorsairKeyboardKeyId key in customxmlfunctions.ReadCustomKeys.customKeys){
					setKeyColor(customxmlfunctions.ReadCustomKeys.customColors[i], key);
					i++;
				}
			}catch(Exception e){
				Program.errorWrite("Error in CorsairKeyboardAPI.colorCustomKeys: "+e.Message);
			}
		}
		#endregion

		public static CorsairKeyboard keyboard; // Since we won't be needing this outside this class, I say "let it stay"

		#region Init the keyboard SDK
		/// <summary>
		/// <para>This function will try to start the Corsair SDK</para>
		/// <para>This function will use exclusive access!</para>
		/// </summary>
		/// <returns>If false, then the SDK failed the initialize</returns>
		public static bool initSDK(){
			try{
				CueSDK.Initialize(true);
				keyboard = CueSDK.KeyboardSDK;
				if(keyboard == null)
					throw new WrapperException("No keyboard found!");
				keyboard.UpdateMode = CUE.NET.Devices.Generic.Enums.UpdateMode.Continuous; // No need to call manual update, as we do it automatic
				return true;
			}catch(Exception){
				return false;
			}
		}
		#endregion

		#region Clear all keys
		/// <summary>
		/// Clears/sets all keyboard keys to black. Only useful when you would like to reload the custom keys, or clear ammo
		/// </summary>
		public static void clearAllKeys(){
			foreach(CorsairKey key in keyboard.Keys){ // Loops throught all the RGB keys the keyboard has
				key.Led.Color = Color.Black; // Set the current key to black
			}
		}
		#endregion
	}
}

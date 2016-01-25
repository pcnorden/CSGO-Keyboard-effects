using System;
using System.Drawing;
using CUE.NET;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Enums;
using CUE.NET.Exceptions;

namespace CSGO_Corsair {
	class CorsairKeyboardAPI{
		#region Summary shit for function
		/// <summary>
		/// This function is used AFTER the keyboard SDK have been started!
		/// </summary>
		/// <param name="color">The color(System.Drawing.Color) you want</param>
		/// <param name="key">The CorsairKeyboardId key</param>
		#endregion
		public static void setKeyColor(Color color, CorsairKeyboardKeyId key){
			try{
				keyboard[key].Led.Color = color;
			}catch(Exception e){
				Program.errorWrite("Failed to update key, error msg: "+e.Message);
			}
		}
		public static void setKeyColors(Color color, CorsairKeyboardKeyId[] keys){
			try{
				foreach(CorsairKeyboardKeyId tempKey in keys){
					keyboard[tempKey].Led.Color = color;
				}
			}catch(Exception e){
				Program.errorWrite("Failed to update keyGroup, error msg: "+e.Message);
			}
		}

		private static CorsairKeyboard keyboard;

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
	}
}

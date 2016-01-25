using CSGO_Corsair.PluginLoader;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace CSGO_Corsair {
	class DecodeJson : IPluginHost{
		#region IPluginHost	interface implementation
		public dynamic publicData {get{return Data;}}
		public CorsairKeyboard keyboard {get{return CorsairKeyboardAPI.keyboard;}}
		#endregion
		public List<IPlugin> keyboardPlugins {get; private set;}
		private dynamic Data;

		public static bool isPluginsLoaded = false; // value to see if the plugins are loaded
		public static bool areTherePlugins = false; // Value set to true if plugins exists, else false, so we don't try to call methods that don't exists

		public void LoadPlugins(){
			keyboardPlugins = new List<IPlugin>();
			IPlugin[] plugins = PluginLoader.PluginLoader.loadPlugins();
			if(plugins == null){
				isPluginsLoaded = true;
				areTherePlugins = false;
				return;
			}
			foreach(IPlugin currentPlugin in plugins){
				currentPlugin.Host = this;
				keyboardPlugins.Add(currentPlugin);
			}
			isPluginsLoaded = true;
			areTherePlugins = true;
		}
		public void updateWithPlugins(dynamic data){
			Data = data;
			List<Modes> buffer = new List<Modes>();
			foreach(IPlugin currentPlugin in keyboardPlugins){
				foreach(Modes currentMode in currentPlugin.ModeList){
					buffer.Add(currentMode);
				}
			}
			foreach(Modes currentMode in buffer){
				currentMode.update();
			}
		}
		public void paint_keys(dynamic data){
			if(isPluginsLoaded == false) // Checks if we have loaded the plugins, and if not, load the plugins
				LoadPlugins();
			if(areTherePlugins == true)
				updateWithPlugins(data); // After we have loaded the plugins, we will trigger all plugins, and send the game data with it.
			CorsairKeyboardKeyId[] keys = Program.keys;
			CorsairKeyboardKeyId[] FKeys = {CorsairKeyboardKeyId.F1, CorsairKeyboardKeyId.F2, CorsairKeyboardKeyId.F3, CorsairKeyboardKeyId.F4, CorsairKeyboardKeyId.F5, CorsairKeyboardKeyId.F6, CorsairKeyboardKeyId.F7, CorsairKeyboardKeyId.F8, CorsairKeyboardKeyId.F9, CorsairKeyboardKeyId.F10, CorsairKeyboardKeyId.F11, CorsairKeyboardKeyId.F12};
			// Weapon 0
			try{
				if(data.player.weapons.weapon_0.state == "active" || data.player.weapons.weapon_0.state == "reloading"){
					if(data.player.weapons.weapon_0.type == "Knife" || data.player.weapons.weapon_0.type == "Grenade"){ // If the weapon in slot 0 is grenade/knife, then paint the key white
						CorsairKeyboardAPI.setKeyColor(Color.White, keys[0]);
					}else{ // If the weapon in slot 0 is a weapon, then calculate how much ammunation we have left
						float ammo = data.player.weapons.weapon_0.ammo_clip; // Get the current ammo
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_0.ammo_clip_max, 0, 120); // Remap the whole value so we can color with HSV
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 1), keys[0]); // Color the key
					}
				}else if(data.player.weapons.weapon_0.state == "holstered"){ // If the weapon is inactive
					if(data.player.weapons.weapon_0.type == "Knife" || data.player.weapons.weapon_0.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(0, 0, 0.5), keys[0]);
					}else{
						float ammo = data.player.weapons.weapon_0.amm0_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_0.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 0.5), keys[0]);
					}
				}
			}catch(Exception){ // If the data doesn't exists, make the key black.
				CorsairKeyboardAPI.setKeyColor(Color.Black, keys[0]);
			}

			// Weapon 1
			try{
				if(data.player.weapons.weapon_1.state == "active" || data.player.weapons.weapon_1.state == "reloading"){ // Check if the weapon is active
					if(data.player.weapons.weapon_1.type == "Knife" || data.player.weapons.weapon_1.type == "Grenade"){ // And if it's a grenade/knife
						CorsairKeyboardAPI.setKeyColor(Color.White, keys[0]); // If grenade/knife, paint it white
					}else{ // If it's not a grenade/knife, calculate ammunation
						float ammo = data.player.weapons.weapon_1.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_1.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 1), keys[1]); // Color the key
					}
				}else if(data.player.weapons.weapon_1.state == "holstered"){ // IF the weapon is inactive, we will however check what it is, and dim the light
					if(data.player.weapons.weapon_1.type == "Knife" || data.player.weapons.weapon_1.state == "Grenade"){ // Grenade or knife?
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(0, 0, 0.5), keys[1]);
					}else{ // If not grenade/knife
						float ammo = data.player.weapons.weapon_1.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_1.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 0.5), keys[1]);
					}
				}
			}catch(Exception){ // If the weapon don't exists, color the key black
				CorsairKeyboardAPI.setKeyColor(Color.Black, keys[0]);
			}

			// Weapon 2
			try{
				if(data.player.weapons.weapon_2.state == "active" || data.player.weapons.weapon_2.state == "reloading"){
					if(data.player.weapons.weapon_2.type == "Knife" || data.player.weapons.weapon_2.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Color.White, keys[2]);
					}else{
						float ammo = data.player.weapons.weapon_2.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_2.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 1), keys[2]);
					}
				}else if(data.player.weapons.weapon_2.state == "holstered"){
					if(data.player.weapons.weapon_2.type == "Knife" || data.player.weapons.weapon_2.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(0, 0, 0.5), keys[2]);
					}else{
						float ammo = data.player.weapons.weapon_2.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_2.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 0.5), keys[2]);
					}
				}
			}catch(Exception){
				CorsairKeyboardAPI.setKeyColor(Color.Black, keys[2]);
			}

			// Weapon 3
			try{
				if(data.player.weapons.weapon_3.state == "active" || data.player.weapons.weapon_3.state == "reloading"){
					if(data.player.weapons.weapon_3.type == "Knife" || data.player.weapons.weapon_3.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Color.White, keys[3]);
					}else{
						float ammo = data.player.weapons.weapon_3.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_3.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 1), keys[3]);
					}
				}else if(data.player.weapons.weapon_3.state == "holstered"){
					if(data.player.weapons.weapon_3.type == "Knife" || data.player.weapons.weapon_3.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(0, 0, 0.5), keys[3]);
					}else{
						float ammo = data.player.weapons.weapon_3.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_3.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 0.5), keys[3]);
					}
				}
			}catch(Exception){
				CorsairKeyboardAPI.setKeyColor(Color.Black, keys[3]);
			}

			// Weapon 4
			try{
				if(data.player.weapons.weapon_4.state == "active" || data.player.weapons.weapon_4.state == "reloading"){
					if(data.player.weapons.weapon_4.type == "Knife" || data.player.weapons.weapon_4.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Color.White, keys[4]);
					}else{
						float ammo = data.player.weapons.weapon_4.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_4.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 1), keys[4]);
					}
				}else if(data.player.weapons.weapon_4.state == "holstered"){
					if(data.player.weapons.weapon_4.type == "Knife" || data.player.weapons.weapon_4.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(0, 0, 0.5), keys[4]);
					}else{
						float ammo = data.player.weapons.weapon_4.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_4.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 0.5), keys[4]);
					}
				}
			}catch(Exception){
				CorsairKeyboardAPI.setKeyColor(Color.Black, keys[4]);
			}

			// Weapon 5
			try{
				if(data.player.weapons.weapon_5.state == "active" || data.player.weapons.weapon_5.state == "reloading"){
					if(data.player.weapons.weapon_5.type == "Knife" || data.player.weapons.weapon_5.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Color.White, keys[5]);
					}else{
						float ammo = data.player.weapons.weapon_5.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_5.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 1), keys[5]);
					}
				}else if(data.player.weapons.weapon_5.state == "holstered"){
					if(data.player.weapons.weapon_5.type == "Kinfe" || data.player.weapons.weapon_5.type == "Grenade"){
						float ammo = data.player.weapons.weapon_5.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_5.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 0.5), keys[5]);
					}
				}
			}catch(Exception){
				CorsairKeyboardAPI.setKeyColor(Color.Black, keys[5]);
			}

			// Weapon 6
			try{
				if(data.player.weapons.weapon_6.state == "active" || data.player.weapons.weapon_6.state == "reloading"){
					if(data.player.weapons.weapon_6.type == "Knife" || data.player.weapons.weapon_6.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Color.White, keys[6]);
					}else{
						float ammo = data.player.weapons.weapon_6.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_6.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 1), keys[6]);
					}
				}else if(data.player.weapons.weapon_6.state == "holstered"){
					if(data.player.weapons.weapon_6.type == "Knife" || data.player.weapons.weapon_6.type == "Grenade"){
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(0, 0, 0.5), keys[6]);
					}else{
						float ammo = data.player.weapon.weapon_6.ammo_clip;
						ammo = ammo.remap(0, (float)data.player.weapons.weapon_6.ammo_clip_max, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(ammo, 1, 0.5), keys[6]);
					}
				}
			}catch(Exception){
				CorsairKeyboardAPI.setKeyColor(Color.Black, keys[6]);
			}

			// Health
			try{
				float health = data.player.state.health;
				health = health.remap(0, 100, 0, 120);
				CorsairKeyboardAPI.setKeyColors(Program.ColorFromHSV(health, 1, 1), FKeys);
			}catch(Exception){
				CorsairKeyboardAPI.setKeyColors(Color.Black, FKeys);
			}
		}
	}
	public static class Extensions{
		public static float remap(this float value, float from1, float to1, float from2, float to2){
			return from2+(value-from1)*(to2-from2)/(to1-from1);
		}
	}
}
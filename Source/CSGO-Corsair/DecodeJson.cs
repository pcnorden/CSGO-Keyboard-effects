using CUE.NET.Devices.Keyboard.Enums;
using System;
using System.Drawing;

namespace CSGO_Corsair {
	class DecodeJson{

		private static Color team = Color.Gray;

		/// <summary>
		/// Gets all weapons in inventory, and checks if active/reloading, then returning wich weapon it is
		/// </summary>
		/// <param name="player">Use the decoded json text as argument here</param>
		/// <returns>Will return -1 if no weapon is active</returns>
		public static int current_weapon(dynamic player){
			#region Weapon check
			// weapon 0
			try {
				if(player.player.weapons.weapon_0.state == "active" || player.player.weapons.weapon_0.state == "reloading")
					return 0;
			}catch(Exception){

			}

			// weapon 1
			try{
				if(player.player.weapons.weapon_1.state == "active" || player.player.weapons.weapon_1.state == "reloading")
					return 1;
			}catch(Exception){

			}

			// weapon 2
			try{
				if(player.player.weapons.weapon_2.state == "active" || player.player.weapons.weapon_2.state == "reloading")
					return 2;
			}catch(Exception){

			}

			// weapon 3
			try{
				if(player.player.weapons.weapon_3.state == "active" || player.player.weapons.weapon_3.state == "reloading")
					return 3;
			}catch(Exception){

			}

			// weapon 4
			try{
				if(player.player.weapons.weapon_4.state == "active" || player.player.weapons.weapon_4.state == "reloading")
					return 4;
			}catch(Exception){

			}

			// weapon 5
			try{
				if(player.player.weapons.weapon_5.state == "active" || player.player.weapons.weapon_5.state == "reloading")
					return 5;
			}catch(Exception){

			}

			// weapon 6
			try{
				if(player.player.weapons.weapon_6.state == "active" || player.player.weapons.weapon_6.state == "reloading")
					return 6;
			}catch(Exception){

			}
			#endregion

			// return -1 if no weapon is active
			return -1;
		}
		public static int get_ammo(dynamic data, int weapon){
			try{
				if(data.player.team == "CT"){
					team = Color.Cyan;
				}else if(data.player.team == "T"){
					team = Color.Orange;
				}
			}catch(Exception){}
			#region Compare and return
			//weapon 0
			if(weapon == 0){
				try{
					if(data.player.weapons.weapon_0.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_0.type == "Grenade")
						return 1;
					else
						return data.player.weapons_0.ammo_clip;
				}catch(Exception){}
			}

			//weapon 1
			if(weapon == 1){
				try{
					if(data.player.weapons.weapon_1.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_1.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_1.ammo_clip;
				}catch(Exception){}
			}

			//weapon 2
			if(weapon == 2){
				try{
					if(data.player.weapons.weapon_2.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_2.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_2.ammo_clip;
				}catch(Exception){}
			}

			//weapon 3
			if(weapon == 3){
				try{
					if(data.player.weapons.weapon_3.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_4.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_3.ammo_clip;
				}catch(Exception){}
			}

			//weapon 4
			if(weapon == 4){
				try{
					if(data.player.weapons.weapon_4.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_4.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_4.ammo_clip;
				}catch(Exception){}
			}

			//weapon 5
			if(weapon == 5){
				try{
					if(data.player.weapons.weapon_5.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_5.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_5.ammo_clip;
				}catch(Exception){}
			}

			//weapon 6
			if(weapon == 6){
				try{
					if(data.player.weapons.weapon_6.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_6.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_6.ammo_clip;
				}catch(Exception){}
			}
			#endregion

			return -1;
		}
		public static int get_max_ammo(dynamic data, int weapon){
			#region Compare and return
			if(weapon == 0){
				try{
					if(data.player.weapons.weapon_0.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_0.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_0.ammo_clip_max;
				}catch(Exception){}
			}else if(weapon == 1){
				try{
					if(data.player.weapons.weapon_1.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_1.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_1.ammo_clip_max;
				}catch(Exception){}
			}else if(weapon == 2){
				try{
					if(data.player.weapons.weapon_2.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_2.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_2.ammo_clip_max;
				}catch(Exception){}
			}else if(weapon == 3){
				try{
					if(data.player.weapons.weapon_3.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_3.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_3.ammo_clip_max;
				}catch(Exception){}
			}else if(weapon == 4){
				try{
					if(data.player.weapons.weapon_4.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_4.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_4.ammo_clip_max;
				}catch(Exception){}
			}else if(weapon == 5){
				try{
					if(data.player.weapons.weapon_5.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_5.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_5.ammo_clip_max;
				}catch(Exception){}
			}else if(weapon == 6){
				try{
					if(data.player.weapons.weapon_6.type == "Knife")
						return -1;
					else if(data.player.weapons.weapon_6.type == "Grenade")
						return 1;
					else
						return data.player.weapons.weapon_6.ammo_clip_max;
				}catch(Exception){}
			}
			#endregion
			return -1;
		}

		public static void paint_ammo(int weapon, int currentAmmo, int maxAmmo, dynamic data){
			CorsairKeyboardKeyId Key1 = Program.keys[0];
			CorsairKeyboardKeyId Key2 = Program.keys[1];
			CorsairKeyboardKeyId Key3 = Program.keys[2];
			CorsairKeyboardKeyId Key4 = Program.keys[3];
			CorsairKeyboardKeyId Key5 = Program.keys[4];
			CorsairKeyboardKeyId Key6 = Program.keys[5];
			CorsairKeyboardKeyId Key7 = Program.keys[6];

			CorsairKeyboardKeyId[] FKeys = {CorsairKeyboardKeyId.F1, CorsairKeyboardKeyId.F2, CorsairKeyboardKeyId.F3, CorsairKeyboardKeyId.F4,
			CorsairKeyboardKeyId.F5, CorsairKeyboardKeyId.F6, CorsairKeyboardKeyId.F7, CorsairKeyboardKeyId.F8,
			CorsairKeyboardKeyId.F9, CorsairKeyboardKeyId.F10, CorsairKeyboardKeyId.F11, CorsairKeyboardKeyId.F12};

			CorsairKeyboardKeyId[] gamingkeys = {CorsairKeyboardKeyId.W, CorsairKeyboardKeyId.A, CorsairKeyboardKeyId.S, CorsairKeyboardKeyId.D, CorsairKeyboardKeyId.Escape};
			
			CorsairKeyboardAPI.setKeyColors(team, gamingkeys);
			try{
				float health = data.player.state.health;
				health = health.remap(0, 100, 0, 120);
				CorsairKeyboardAPI.setKeyColors(Program.ColorFromHSV(health,1,1), FKeys);
			}catch(Exception){}

			switch(weapon){
				case -1:
					CorsairKeyboardAPI.setKeyColor(Color.Black, Key1);
					CorsairKeyboardAPI.setKeyColor(Color.Black, Key2);
					CorsairKeyboardAPI.setKeyColor(Color.Black, Key3);
					CorsairKeyboardAPI.setKeyColor(Color.Black, Key4);
					CorsairKeyboardAPI.setKeyColor(Color.Black, Key5);
					CorsairKeyboardAPI.setKeyColor(Color.Black, Key6);
					CorsairKeyboardAPI.setKeyColor(Color.Black, Key7);
					break;
				case 0:
					if((currentAmmo == -1 && maxAmmo == -1) || (currentAmmo == 1 && maxAmmo == 1)){
						CorsairKeyboardAPI.setKeyColor(Color.White, Key1);
					}else{
						float magasin = currentAmmo;
						magasin = magasin.remap(0, maxAmmo, 120, 0);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(magasin, 1, 1), Key1);
					}
					break;
				case 1:
					if((currentAmmo == -1 && maxAmmo == -1) || (currentAmmo == 1 && maxAmmo == 1)){
						CorsairKeyboardAPI.setKeyColor(Color.White, Key2);
					}else{
						float magasin = currentAmmo;
						magasin = magasin.remap(0, maxAmmo, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(magasin, 1, 1), Key2);
					}
					break;
				case 2:
					if((currentAmmo != -1 && maxAmmo != -1) || (currentAmmo != 1 && maxAmmo != 1)){
						float magasin = currentAmmo;
						magasin = magasin.remap(0, maxAmmo, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(magasin,1,1), Key3);
					}else{
						CorsairKeyboardAPI.setKeyColor(Color.White, Key3);
					}
					break;
				case 3:
					if((currentAmmo != -1 && maxAmmo != -1) || (currentAmmo != 1 && maxAmmo != 1)){
						float magasin = currentAmmo;
						magasin = magasin.remap(0, maxAmmo, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(magasin,1,1), Key4);
					}else{
						CorsairKeyboardAPI.setKeyColor(Color.White, Key4);
					}
					break;
				case 4:
					if((currentAmmo != -1 && maxAmmo != -1) || (currentAmmo != 1 && maxAmmo != 1)){
						float magasin = currentAmmo;
						magasin = magasin.remap(0, maxAmmo, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(magasin,1,1), Key5);
					}else{
						CorsairKeyboardAPI.setKeyColor(Color.White, Key5);
					}
					break;
				case 5:
					if((currentAmmo != -1 && maxAmmo != -1) || (currentAmmo != 1 && maxAmmo != 1)){
						float magasin = currentAmmo;
						magasin = magasin.remap(0, maxAmmo, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(magasin,1,1), Key6);
					}else{
						CorsairKeyboardAPI.setKeyColor(Color.White, Key6);
					}
					break;
				case 6:
					if((currentAmmo != -1 && maxAmmo != -1) || (currentAmmo != 1 && maxAmmo != 1)){
						float magasin = currentAmmo;
						magasin = magasin.remap(0, maxAmmo, 0, 120);
						CorsairKeyboardAPI.setKeyColor(Program.ColorFromHSV(magasin,1,1), Key7);
					}else{
						CorsairKeyboardAPI.setKeyColor(Color.White, Key7);
					}
					break;
			}
		}
	}
	public static class Extensions{
			public static float remap(this float value, float from1, float to1, float from2, float to2){
				return from2+(value-from1)*(to2-from2)/(to1-from1);
			}
		}
}
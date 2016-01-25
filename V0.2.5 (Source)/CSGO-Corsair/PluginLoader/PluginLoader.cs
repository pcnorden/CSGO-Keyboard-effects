using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSGO_Corsair.PluginLoader {
	static class PluginLoader{
		private static string dir = "plugins";
		internal static IPlugin[] loadPlugins(){
			if(!Directory.Exists(dir)) return null;

			List<IPlugin> pluginList = new List<IPlugin>();
			// Attempt to load all plugins
			foreach(string currentPluginPath in Directory.EnumerateFiles(dir, "*.dll")){
				Assembly pluginAssembly = TryLoadAssembly(currentPluginPath);
				if(pluginAssembly == null) continue; // Unable to load assembly

				// Load all IPlugin instances in the current assembly
				var iPluginTypes = pluginAssembly.GetTypes().Where(type => typeof(IPlugin).IsAssignableFrom(type) && type.IsClass);
				foreach(Type currentPlugin in iPluginTypes){
					try{
						IPlugin createdInstance = (IPlugin)Activator.CreateInstance(currentPlugin);
						pluginList.Add(createdInstance);
					}catch{}
				}
			}
			return pluginList.ToArray();
		}
		private static Assembly TryLoadAssembly(string filepath){
			try{
				Assembly assembly = Assembly.LoadFile(Path.GetFullPath(filepath));
				return assembly;
			}catch(Exception e){
				Program.errorWrite("Failed to load plugin! Error code: "+e.Message);
				return null;
			}
		}
	}
}
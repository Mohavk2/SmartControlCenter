using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HostWebUI
{
    public static class PluginLoader
    {
        private static string pluginDirectoryPath = @"C:\Users\Saint\source\repos\SmartControlCenter\HostWebUI\bin\Debug\net5.0\Plugins\net5.0";

        public static List<Assembly> GetPluginAssamblies()
        {
            string [] pluginsPaths = Directory.GetFiles(pluginDirectoryPath, "*.dll");
            List<Assembly> pluginAssemblies = new List<Assembly>();

            foreach(var path in pluginsPaths)
            {
                Assembly assembly = Assembly.LoadFrom(path);
                if(assembly.ContainsPlugin(typeof(IPlugin)))
                    pluginAssemblies.Add(assembly);
            }
            return pluginAssemblies;
        }

        static bool ContainsPlugin(this Assembly assembly, Type pluginType)
        {
            Type[] types = assembly.GetTypes();
            foreach(var type in types)
            {
                if(type.IsAssignableTo(pluginType))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

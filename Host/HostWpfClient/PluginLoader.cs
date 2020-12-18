using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using WpfInfrastructure;

namespace HostWpfClient
{
    public static class PluginLoader
    {   
        //TODO: Add configuration
        public const string pluginDirectoryPath = @"C:\Users\Saint\source\repos\SmartControlCenter\Host\HostWpfClient\bin\Debug\net5.0-windows\Plugins\net5.0-windows";

        public static List<IWpfPlugin> LoadPlugins()
        {
            string[] pluginsPaths = Directory.GetFiles(pluginDirectoryPath, "*.dll");

            List<IWpfPlugin> plugins = new List<IWpfPlugin>();

            var assemblies = GetPluginAssemblies(pluginsPaths);

            foreach (var assembly in assemblies)
            {
                try
                {
                    var plugin = TryGetPlugin(assembly);
                    plugins.Add(plugin);
                }
                catch (Exception ex)
                {
                    //TODO: Add logging
                }
            }
            return plugins;
        }

        private static IWpfPlugin TryGetPlugin(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {   
                //Load only plugins
                if (type.IsAssignableTo(typeof(IWpfPlugin)))
                {
                    return (IWpfPlugin)Activator.CreateInstance(type);
                }
            }
            throw new Exception($"Unable to load plugin from assembly {assembly.FullName}");
        }

        private static List<Assembly> GetPluginAssemblies(string[] paths)
        {
            List<Assembly> assemblies = new List<Assembly>();

            foreach (var path in paths)
            {
                assemblies.Add(Assembly.LoadFrom(path));
            }
            return assemblies;
        }
    }
}

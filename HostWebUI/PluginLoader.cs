using Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace HostWebUI
{
    public class PluginContext
    {
        public AssemblyName Name { get; private set; }
        public List<ApplicationPart> Parts { get; } = new List<ApplicationPart>();

        public PluginContext(Assembly pluginAssembly)
        {
            this.Name = pluginAssembly.GetName();
            this.Parts = pluginAssembly.GetAllParts();
        }
    }

    public static class PluginLoader
    {
        public const string pluginDirectoryPath = @"C:\Users\Saint\source\repos\SmartControlCenter\HostWebUI\bin\Debug\net5.0\Plugins\net5.0";
        private static Type pluginType = typeof(IPlugin);

        public static List<PluginContext> LoadPlugins()
        {
            string[] pluginsPaths = Directory.GetFiles(pluginDirectoryPath, "*.dll");

            List<PluginContext> plugins = new List<PluginContext>();

            List<Assembly> assemblies = GetPluginAssemblies(pluginsPaths);

            foreach (var assembly in assemblies)
            {
                plugins.Add(new PluginContext(assembly));
            }
            return plugins;
        }

        public static List<Assembly> GetPluginAssemblies(string[] paths)
        {
            List<Assembly> assemblies = new List<Assembly>();

            foreach (var path in paths)
            {
                var assambly = Assembly.LoadFrom(path);
                if (assambly.ContainsPlugin(typeof(IPlugin)))
                {
                    assemblies.Add(assambly);
                }
            }
            return assemblies;
        }

        //Extensions

        static bool ContainsPlugin(this Assembly assembly, Type pluginType)
        {
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAssignableTo(pluginType))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<ApplicationPart> GetAllParts(this Assembly pluginAssembly)
        {
            List<ApplicationPart> allParts = new List<ApplicationPart>();

            var partFactory = ApplicationPartFactory.GetApplicationPartFactory(pluginAssembly);

            foreach (var part in partFactory.GetApplicationParts(pluginAssembly))
            {
                allParts.Add(part);
            }

            var relatedAttributes = pluginAssembly.GetCustomAttributes<RelatedAssemblyAttribute>();

            foreach (var attribute in relatedAttributes)
            {
                var assembly = Assembly.Load(attribute.AssemblyFileName);
                partFactory = ApplicationPartFactory.GetApplicationPartFactory(assembly);

                foreach (var part in partFactory.GetApplicationParts(assembly))
                {
                    allParts.Add(part);
                }
            }
            return allParts;
        }
    }
}

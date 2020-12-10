using Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace HostWebUI
{
    public struct PluginLoadedPackage
    {
        public string AssembleName { get; init; }
        public IPlugin Plugin { get; init; }
        public List<ApplicationPart> Parts { get; init; }
        public EmbeddedFileProvider FileProvider { get; init; }
        public string StaticFilesPath { get; init; }
    }

    public static class PluginLoader
    {
        public const string pluginDirectoryPath = @"C:\Users\Saint\source\repos\SmartControlCenter\Host\HostWebUI\bin\Debug\net5.0\Plugins\net5.0";

        public static List<PluginLoadedPackage> LoadPlugins()
        {
            string[] pluginsPaths = Directory.GetFiles(pluginDirectoryPath, "*.dll");

            List<PluginLoadedPackage> plugins = new List<PluginLoadedPackage>();

            var assemblies = GetPluginAssemblies(pluginsPaths);

            foreach (var assembly in assemblies)
            {
                try
                {
                    var plugin = TryGetPlugin(assembly);
                    var name = assembly.GetName().Name;
                    var parts = GetAllParts(assembly);
                    var staticFilesPath = name + ".wwwroot";
                    var fileProvider = new EmbeddedFileProvider(assembly, staticFilesPath);

                    var pluginPackage = new PluginLoadedPackage
                    {
                        Plugin = plugin,
                        AssembleName = name,
                        Parts = parts,
                        StaticFilesPath = staticFilesPath,
                        FileProvider = fileProvider
                    };

                    plugins.Add(pluginPackage);
                }
                catch (Exception ex)
                {
                    //TODO:Add logging
                }
            }
            return plugins;
        }

        private static IPlugin TryGetPlugin(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAssignableTo(typeof(IPlugin)))
                {
                    return (IPlugin)Activator.CreateInstance(type);
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

        private static List<ApplicationPart> GetAllParts(Assembly pluginAssembly)
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

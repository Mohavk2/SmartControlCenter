using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using WebInfrastructure;

namespace HostWeb.Services
{
    public class PluginResourcesProvider
    {
        //TODO: Add configuration
        public const string pluginDirectoryPath = @"C:\Users\Saint\source\repos\SmartControlCenter\Host\HostWeb\bin\Debug\net5.0\Plugins\net5.0";

        public List<Assembly> Assemblies { get; set; } = new List<Assembly>();
        public List<IWebPluginLoader> PluginLoaders { get; set; } = new List<IWebPluginLoader>();
        public List<Type> WebPluginTypes { get; set; } = new List<Type>();
        public List<ApplicationPart> PluginParts { get; set; } = new List<ApplicationPart>();

        public void LoadPluginsWithViews()
        {
            List<(Assembly, Assembly)> pluginValuePairs = new List<(Assembly, Assembly)>();
            string[] libraryPaths = Directory.GetFiles(pluginDirectoryPath, "*.dll");

            var assemblies = GetAssemblies(libraryPaths);

            foreach (var assembly in assemblies)
            {
                try
                {
                    Type[] types = GetTypes(assembly);
                    if(assembly.FullName.Contains("Views") == false)
                    {
                        try
                        {
                            PluginLoaders.Add(GetLoader(types));
                        }
                        catch (Exception ex)
                        {
                            //TODO: Add logging
                        }
                        try
                        {
                            WebPluginTypes.Add(GetPluginType(types));
                        }
                        catch (Exception ex)
                        {
                            //TODO: Add logging
                        }
                    }
                    try
                    {
                        PluginParts.AddRange(GetApplicationParts(assembly));
                    }
                    catch (Exception ex)
                    {
                        //TODO: Add logging
                    }
                    Assemblies.Add(assembly);
                }
                catch(Exception ex)
                {
                    //TODO: Add logging
                }
            }
        }

        List<Assembly> GetAssemblies(string[] paths)
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var path in paths)
            {
                assemblies.Add(Assembly.LoadFrom(path));
            }
            return assemblies;
        }

        private Type[] GetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch(Exception ex)
            {
                throw new Exception($"Unable to get types from an assembly", ex);
            }
        }

        private IWebPluginLoader GetLoader(Type[] types)
        {
            try
            {
                var loaderType = (from type in types
                                  where type.IsAssignableTo(typeof(IWebPluginLoader))
                                  select type).First();
                return (IWebPluginLoader)Activator.CreateInstance(loaderType);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get plugin loader from an assembly", ex);
            }
        }

        private Type GetPluginType(Type[] types)
        {
            try
            {
                return (from type in types
                        where type.IsAssignableTo(typeof(IWebPlugin))
                        select type).First();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get plugin type from an assembly", ex);
            }
        }

        List<ApplicationPart> GetApplicationParts(Assembly pluginAssembly)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"Unable to get application parts an assembly", ex);
            }
        }
    }
}

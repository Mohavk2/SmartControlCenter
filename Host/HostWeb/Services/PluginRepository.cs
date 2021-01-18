using AutoMapper;
using CommonInfrastructure.DTO;
using HostData.Models;
using HostWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInfrastructure;

namespace HostWeb.Services
{
    public class PluginRepository : IPluginRepository
    {
        private static Dictionary<string, IWebPlugin> plugins = new Dictionary<string, IWebPlugin>();

        IMapper mapper;

        public PluginRepository(IMapper mapper, PluginResourcesProvider pluginResources, IServiceProvider services)
        {
            this.mapper = mapper;
            foreach (var type in pluginResources.WebPluginTypes)
            {
                var plugin = services.GetService(type) as IWebPlugin ?? throw new Exception($"The service {type} was not found in the DI container");
                //TODO: Add logging
                plugins.Add(plugin.GetName(), plugin);
            }
        }

        public IEnumerable<string> GetPluginNames()
        {
            return plugins.Keys;
        }

        public List<PluginScript> GetAllScripts()
        {
            List <PluginScript> scripts = new List<PluginScript>();
            foreach (var plugin in plugins.Values)
            {
                scripts.AddRange(mapper.Map(plugin.GetScripts(), new List<PluginScript>() , opt => opt.AfterMap((src, dest) => {
                    foreach (var dto in dest)
                    {
                        dto.PluginName = plugin.GetName();
                    }
                })));
            }
            return scripts;
        }

        public IEnumerable<IWebPlugin> GetPlugins()
        {
            return plugins.Values;
        }
    }
}

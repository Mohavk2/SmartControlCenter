using AutoMapper;
using CommonInfrastructure.DTO;
using HostWeb.Entities;
using HostWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInfrastructure;

namespace HostWeb.Services
{
    public class PluginManager : IPluginManager
    {
        private static Dictionary<string, IWebPlugin> plugins = new Dictionary<string, IWebPlugin>();

        IMapper mapper;

        public PluginManager(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void AddPlugin(IWebPlugin plugin)
        {
            plugins.Add(plugin.GetName(), plugin);
        }

        public IEnumerable<string> GetPluginNames()
        {
            return plugins.Keys;
        }

        public List<Script> GetAllScripts()
        {
            List <Script> scripts = new List<Script>();
            foreach (var plugin in plugins.Values)
            {
                scripts.AddRange(mapper.Map(plugin.GetScripts(), new List<Script>() , opt => opt.AfterMap((src, dest) => {
                    foreach (var dto in dest)
                    {
                        dto.PluginId = plugin.Id;
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

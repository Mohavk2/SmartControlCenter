using HostWeb.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Services
{
    public class PluginManager : IPluginManager
    {
        private static Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();

        public void AddPlugin(IPlugin plugin)
        {
            plugins.Add(plugin.Name, plugin);
        }

        public IEnumerable<string> GetPluginNames()
        {
            return plugins.Keys;
        }

        public IEnumerable<IPlugin> GetPlugins()
        {
            return plugins.Values;
        }
    }
}

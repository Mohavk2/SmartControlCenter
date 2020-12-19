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

        public void AddPlugin(IWebPlugin plugin)
        {
            plugins.Add(plugin.GetName(), plugin);
        }

        public IEnumerable<string> GetPluginNames()
        {
            return plugins.Keys;
        }

        public IEnumerable<IWebPlugin> GetPlugins()
        {
            return plugins.Values;
        }
    }
}

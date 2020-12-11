using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Interfaces
{
    public interface IPluginManager
    {
        public void AddPlugin(IPlugin plugin);
        public IEnumerable<string> GetPluginNames();
        public IEnumerable<IPlugin> GetPlugins();
    }
}

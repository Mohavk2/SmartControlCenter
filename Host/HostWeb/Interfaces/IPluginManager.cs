using HostWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInfrastructure;

namespace HostWeb.Interfaces
{
    public interface IPluginManager
    {
        public void AddPlugin(IWebPlugin plugin);
        public IEnumerable<string> GetPluginNames();
        public IEnumerable<IWebPlugin> GetPlugins();
        public List<Script> GetAllScripts();
    }
}

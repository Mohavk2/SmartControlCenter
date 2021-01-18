using HostData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInfrastructure;

namespace HostWeb.Interfaces
{
    public interface IPluginRepository
    {
        public IEnumerable<string> GetPluginNames();
        public IEnumerable<IWebPlugin> GetPlugins();
        public List<PluginScript> GetAllScripts();
    }
}

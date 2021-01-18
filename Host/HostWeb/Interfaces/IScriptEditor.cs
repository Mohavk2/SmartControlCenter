using HostData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Interfaces
{
    public interface IScriptEditor
    {
        public HostScript GetHostScript();

        public void AddPluginScript(PluginScript pluginScript);

        public void RemovePluginScript(PluginScript pluginScript);
    }
}

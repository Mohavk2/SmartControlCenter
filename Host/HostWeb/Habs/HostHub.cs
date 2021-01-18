using HostWeb.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Habs
{
    public class HostHub : Hub
    {
        IPluginRepository pluginManager;

        public HostHub(IPluginRepository pluginManager)
        {
            this.pluginManager = pluginManager;
        }

        public Task GetScripts()
        {
            return Clients.All.SendAsync("ReceiveAllScripts", pluginManager.GetAllScripts());
        }

        public Task RunScript(string scriptName)
        {
            return Clients.All.SendAsync("ScriptRun", scriptName);
        }

        public Task StopScript(string scriptName)
        {
            return Clients.All.SendAsync("ScriptStoped", scriptName);
        }
    }
}

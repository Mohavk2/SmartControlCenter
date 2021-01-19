using AutoMapper;
using CommonInfrastructure.DTO;
using HostData.Models;
using HostWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Services
{
    public class ScriptProcessor : IScriptProcessor
    {
        public bool isScriptRunning { get; set; } = false;

        IPluginManager pluginManager;
        IMapper mapper;

        public ScriptProcessor(IPluginManager pluginManager, IMapper mapper)
        {
            this.pluginManager = pluginManager;
            this.mapper = mapper;
        }

        public Task RunScriptAsync(HostScript hostScript)
        {
            List<Task> scriptsTasks = new List<Task>();

            foreach (var plugin in pluginManager.GetPlugins())
            {
                var pluginName = plugin.GetName();
                if (hostScript.PluginsScripts.ContainsKey(pluginName))
                {
                    foreach(var script in hostScript.PluginsScripts[pluginName])
                    {
                        scriptsTasks.Add(Task.Run(async ()=> 
                        {
                            //To execute each script in its time
                            await Task.Delay(script.BeginsAt);
                            await plugin.RunScriptAsync(mapper.Map(script, new ScriptDTO()));
                        }));
                    }
                }
            }
            return Task.WhenAll(scriptsTasks);
        }

        public void StopRunningScript()
        {
            //TODO: Add implementation
        }
    }
}

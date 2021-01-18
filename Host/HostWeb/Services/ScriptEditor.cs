using HostData.Models;
using HostWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Services
{
    public class ScriptEditor : IScriptEditor
    {
        HostScript hostScript = new HostScript();
        List<int> scriptsEndingValues { get; set; } = new List<int>();

        public ScriptEditor()
        {
            //this.hostScript = hostScript ?? throw new Exception("ScriptEditor: HostScript can't be null");
        }

        public HostScript GetHostScript() => hostScript;

        public void AddPluginScript(PluginScript pluginScript)
        {
            if (pluginScript == null)
            {
                throw new Exception("ScriptEditor: PluginScript can't be null");
            }

            if (!hostScript.PluginsScripts.ContainsKey(pluginScript.PluginName))
            {
                hostScript.PluginsScripts[pluginScript.PluginName] = new List<PluginScript>
                {
                    pluginScript
                };
            }
            else
            {
                if (HasPluginTimeCollissionProblems(pluginScript))
                {
                    throw new Exception("ScriptEditor: can't run two scripts of one plugin at the same time");
                }

                hostScript.PluginsScripts[pluginScript.PluginName].Add(pluginScript);
            }
            scriptsEndingValues.Add(pluginScript.EndsAt);
            hostScript.Duration = scriptsEndingValues.Max();
        }

        public void RemovePluginScript(PluginScript pluginScript)
        {
            if (pluginScript == null)
            {
                throw new Exception("ScriptEditor: PluginScript can't be null");
            }
                
            hostScript.PluginsScripts[pluginScript.PluginName].Remove(pluginScript);

            scriptsEndingValues.Remove(pluginScript.EndsAt);
            hostScript.Duration = scriptsEndingValues.Max();
        }

        bool HasPluginTimeCollissionProblems(PluginScript pluginScript)
        {
            var pluginScripts = hostScript.PluginsScripts[pluginScript.PluginName];

            foreach(var existingScript in pluginScripts)
            {
                bool isOk = (pluginScript.BeginsAt > existingScript.EndsAt || pluginScript.EndsAt < existingScript.BeginsAt);
                if (!isOk)
                {
                    return true;
                }               
            }
            return false;
        }
    }
}

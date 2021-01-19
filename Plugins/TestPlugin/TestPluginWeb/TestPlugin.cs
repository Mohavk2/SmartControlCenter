using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using TestPluginWeb.Hubs;
using WebInfrastructure;
using System.Collections.Generic;
using CommonInfrastructure.DTO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using AutoMapper;
using TestPluginWeb.Services;
using TestPluginWeb.Interfaces;

namespace TestPlugin
{
    public class TestPlugin : IWebPlugin
    {
        public int Id { get; set; }
        public string GetName() => "Test";

        RemoteScriptController remoteScriptController;

        public TestPlugin(RemoteScriptController remoteScriptController)
        {
            this.remoteScriptController = remoteScriptController;
        }

        public IEnumerable<ScriptDTO> GetScripts()
        {
            return remoteScriptController.GetScripts();
        }

        public async Task RunScriptAsync(ScriptDTO script)
        {
            await remoteScriptController.RunScriptAsync(script);
        }
    }
}

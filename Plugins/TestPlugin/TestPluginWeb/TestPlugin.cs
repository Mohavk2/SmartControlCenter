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
        ScriptExecutor commandExecutor;
        ActionExecutor actionExecutor;

        public TestPlugin(RemoteScriptController remoteScriptController, ScriptExecutor commandExecutor, ActionExecutor actionExecutor)
        {
            this.commandExecutor = commandExecutor;
            this.remoteScriptController = remoteScriptController;
            this.actionExecutor = actionExecutor;
        }

        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            //TODO: Rename hubs
            endpoints.MapHub<TestScriptHub>("/TestScript");
            endpoints.MapHub<TestCommandHub>("/TestCommand");
            endpoints.MapHub<TestActionHub>("/TestAction");
        }

        public async Task InitializeAsync()
        {
            await remoteScriptController.ConnectToHubAsync();
            await commandExecutor.ConnectToHubAsync();
            await actionExecutor.ConnectToHubAsync();
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

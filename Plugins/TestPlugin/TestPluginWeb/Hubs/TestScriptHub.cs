using AutoMapper;
using CommonInfrastructure.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;
using TestPluginWeb.Interfaces;
using TestPluginWeb.Repositories;
using TestPluginWeb.Services;

namespace TestPluginWeb.Hubs
{
    public class TestScriptHub : Hub
    {
        readonly ScriptRepository scriptRepository;
        readonly ScriptExecutor scriptExecutor;

        public TestScriptHub(ScriptRepository scriptRepository, ScriptExecutor scriptExecutor)
        {
            this.scriptRepository = scriptRepository;
            this.scriptExecutor = scriptExecutor;
        }

        public Task Get(int id)
        {
            var script = scriptRepository.Get(id);
            return Clients.Caller.SendAsync("GetScript", script);
        }

        public Task Add(ScriptP script)
        {
            scriptRepository.Add(script);
            return Clients.All.SendAsync("Add", script);
        }

        public Task GetAll()
        {
            var scripts = scriptRepository.GetAll();
            return Clients.Caller.SendAsync("GetScripts", scripts);
        }

        public async Task Run(ScriptP script)
        {
            await scriptExecutor.Run(script);
            await Clients.All.SendAsync("RunScript", script);
        }
    }
}

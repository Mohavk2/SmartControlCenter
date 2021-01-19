using AutoMapper;
using CommonInfrastructure.DTO;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestPluginWeb.Interfaces;
using TestPluginWeb.Repositories;

namespace TestPluginWeb.Services
{
    public class RemoteScriptController : HubConnectionService
    {
        ScriptRepository scriptRepository;
        IMapper mapper;

        public RemoteScriptController(ScriptRepository scriptRepository, IMapper mapper) : base("https://localhost:44371/TestScript")
        {
            this.scriptRepository = scriptRepository;
            this.mapper = mapper;
        }

        public IEnumerable<ScriptDTO> GetScripts()
        {
            var scripts = scriptRepository.GetAll();
            return mapper.Map(scripts, new List<ScriptDTO>());
        }

        public async Task RunScriptAsync(ScriptDTO scriptDTO)
        {
            var script = scriptRepository.Get(scriptDTO.Id);
            await SendAsync("Run", script);
        }
    }
}

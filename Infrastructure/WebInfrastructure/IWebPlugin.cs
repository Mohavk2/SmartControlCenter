using CommonInfrastructure;
using CommonInfrastructure.DTO;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebInfrastructure
{
    public interface IWebPlugin
    {
        public int Id { get; }
        public string GetName();
        public void UseEndpoints(IEndpointRouteBuilder endpoints);
        public IEnumerable<ScriptDTO> GetScripts();
        public Task RunScriptAsync(ScriptDTO script);
    }
}

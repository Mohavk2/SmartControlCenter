using CommonInfrastructure;
using CommonInfrastructure.DTO;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebInfrastructure
{
    public interface IWebPlugin
    {
        public int Id { get; set; }
        public string GetName();
        public IEnumerable<ScriptDTO> GetScripts();
        public Task RunScriptAsync(ScriptDTO script);
    }
}

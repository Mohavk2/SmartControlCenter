using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using YiiColorBulbWeb.Hubs;
using WebInfrastructure;
using System.Collections.Generic;
using CommonInfrastructure.DTO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace YiiColorBulbWeb
{
    public class YiiColorBulbPlugin : IWebPlugin
    {
        public int Id { get; set; }

        public YiiColorBulbPlugin()
        {

        }

        public string GetName() => "YiiColorBulb";

        public IEnumerable<ScriptDTO> GetScripts()
        {
            //TODO: Add script creation logic
            return new List<ScriptDTO> { 
                new ScriptDTO { Id = 1, Name = "YiiLightScript1" }, 
                new ScriptDTO { Id = 2, Name = "YiiLightScript2" } 
            };
        }

        public Task RunScriptAsync(ScriptDTO script)
        {
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public void ConfigureUserServices(IServiceCollection services)
        {
            
        }
    }
}

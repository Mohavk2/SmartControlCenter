using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using TestPluginWeb.Hubs;
using WebInfrastructure;
using System.Collections.Generic;
using CommonInfrastructure.DTO;
using System.Threading.Tasks;

namespace TestPlugin
{
    public class TestPlugin : IWebPlugin
    {
        public int Id { get; }
        public TestPlugin(int id)
        {
            this.Id = id;
        }

        public string GetName() => "Test";

        public IEnumerable<ScriptDTO> GetScripts()
        {
            //TODO: Add script creation logic
            return new List<ScriptDTO> { new ScriptDTO { Id = 1, Name = "TestScript1" }, new ScriptDTO { Id = 2, Name = "TestScript2" } };
        }

        public Task RunScriptAsync(ScriptDTO script)
        {
            return Task.CompletedTask;
        }

        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<TestNotificationHub>("/TestNotification");
        }
    }
}

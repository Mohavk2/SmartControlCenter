using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using TestPluginWeb.Hubs;
using WebInfrastructure;

namespace TestPlugin
{
    public class TestPlugin : IWebPlugin
    {
        public string Name { get; } = "Test";

        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<TestNotificationHub>("/TestNotification");
        }
    }
}

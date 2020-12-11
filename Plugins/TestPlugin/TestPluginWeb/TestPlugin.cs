using Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using TestPluginWeb.Hubs;

namespace TestPlugin
{
    public class TestPlugin : IPlugin
    {
        public string Name { get; } = "Test";

        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<TestNotificationHub>("/TestNotification");
        }
    }
}

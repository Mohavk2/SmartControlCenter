using Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using YiiColorBulbWeb.Hubs;

namespace YiiColorBulbWeb
{
    public class YiiColorBulbPlugin : IPlugin
    {
        public string Name { get; } = "YiiColorBulb";

        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<YiiColorBulbNotificationHub>("/YiiColorBulbNotification");
        }
    }
}

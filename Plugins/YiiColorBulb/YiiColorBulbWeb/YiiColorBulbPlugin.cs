using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using YiiColorBulbWeb.Hubs;
using WebInfrastructure;

namespace YiiColorBulbWeb
{
    public class YiiColorBulbPlugin : IWebPlugin
    {
        public string GetName() => "YiiColorBulb";

        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<YiiColorBulbNotificationHub>("/YiiColorBulbNotification");
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebInfrastructure;
using YiiColorBulbWeb.Hubs;

namespace YiiColorBulbWeb
{
    public class YiiColorBulbPluginLoader : IWebPluginLoader
    {
        public void ConfigureUserServices(IServiceCollection services)
        {

        }

        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<YiiColorBulbNotificationHub>("/YiiColorBulbNotification");
        }
    }
}

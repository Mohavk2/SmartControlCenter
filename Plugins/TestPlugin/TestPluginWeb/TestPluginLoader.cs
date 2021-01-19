using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPluginWeb.Hubs;
using TestPluginWeb.Interfaces;
using TestPluginWeb.Repositories;
using TestPluginWeb.Services;
using WebInfrastructure;

namespace TestPluginWeb
{
    public class TestPluginLoader : IWebPluginLoader
    {
        public void ConfigureUserServices(IServiceCollection services)
        {
            services.AddSingleton<ScriptRepository>();
            services.AddSingleton<ActionRepository>();
            services.AddSingleton<ScriptExecutor>();
            services.AddSingleton<ActionExecutor>();
            services.AddSingleton<RemoteScriptController>();
        }
        public void UseEndpoints(IEndpointRouteBuilder endpoints)
        {
            //TODO: Rename hubs
            endpoints.MapHub<TestScriptHub>("/TestScript");
            endpoints.MapHub<TestCommandHub>("/TestCommand");
            endpoints.MapHub<TestActionHub>("/TestAction");
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;

namespace HostWebUI
{
    public class Startup
    {
        private static List<PluginLoadedPackage> plugins;

        public void ConfigureServices(IServiceCollection services)
        {
            plugins = PluginLoader.LoadPlugins();
            foreach (var plugin in plugins)
            {
                foreach(var part in plugin.Parts)
                {
                    services.AddControllersWithViews().PartManager.ApplicationParts.Add(part);
                }
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            foreach (var plugin in plugins)
            {
                var staticFileOptions = new StaticFileOptions
                {
                    FileProvider = plugin.FileProvider,
                    RequestPath = new PathString("/" + plugin.Name + "/wwwroot")
                };
                app.UseStaticFiles(staticFileOptions);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

using HostWebUI.Interfaces;
using HostWebUI.Services;
using Infrastructure;
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
        private static List<PluginLoadedPackage> pluginsPackages;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPluginManager, PluginManager>();

            pluginsPackages = PluginLoader.LoadPlugins();

            foreach (var pluginPackage in pluginsPackages)
            {
                foreach (var part in pluginPackage.Parts)
                {
                    services.AddControllersWithViews().PartManager.ApplicationParts.Add(part);
                }
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IPluginManager pluginManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            foreach (var pluginPackage in pluginsPackages)
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = pluginPackage.FileProvider,
                    RequestPath = new PathString("/" + pluginPackage.AssembleName + "/wwwroot")
                });

                pluginManager.AddPlugin(pluginPackage.Plugin);
            }



            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

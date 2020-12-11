using HostWeb.Interfaces;
using HostWeb.Services;
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

namespace HostWeb
{
    public class Startup
    {
        private static List<PluginLoadedPackage> pluginsPackages;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPluginManager, PluginManager>();

            pluginsPackages = PluginLoader.LoadPlugins();

            //To serve plugins routing and views
            foreach (var package in pluginsPackages)
            {
                foreach (var part in package.Parts)
                {
                    services.AddControllersWithViews().PartManager.ApplicationParts.Add(part);
                }
            }
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IPluginManager pluginManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //To serve plugins css, js and other static resources
            foreach (var package in pluginsPackages)
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new EmbeddedFileProvider(package.Assembly, package.AssembleName + ".wwwroot"),
                    //To avoid plugin resource names collisions
                    RequestPath = new PathString("/" + package.AssembleName + "/wwwroot")
                });
                //Collecting plugins interfaces
                pluginManager.AddPlugin(package.Plugin);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {   //To give the ability to a user to customize endpoints for controllers and hubs
                foreach(var plugin in pluginManager.GetPlugins())
                {
                    plugin.UseEndpoints(endpoints);
                }
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

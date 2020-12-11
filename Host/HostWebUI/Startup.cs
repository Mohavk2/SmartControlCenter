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

            //To serve plugins routing and views
            foreach (var package in pluginsPackages)
            {
                foreach (var part in package.Parts)
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
            {
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}

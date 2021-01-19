using AutoMapper;
using HostData.DataAccess;
using HostWeb.Hubs;
using HostWeb.Interfaces;
using HostWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace HostWeb
{
    public class Startup
    {
        IConfiguration configuration;
        PluginResourcesProvider pluginResourcesProvider;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            pluginResourcesProvider = new PluginResourcesProvider();
            pluginResourcesProvider.LoadPluginsWithViews();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPluginManager, PluginManager>();
            services.AddTransient<IScriptEditor, ScriptEditor>();
            services.AddAutoMapper(config=>
            { 
                config.AddMaps(typeof(Startup));
                config.AddMaps(pluginResourcesProvider.Assemblies);
            });
            services.AddSingleton(provider => pluginResourcesProvider);

            foreach(var type in pluginResourcesProvider.WebPluginTypes)
            {
                services.AddSingleton(type);
            }

            //To serve plugins routing and views
            foreach (var part in pluginResourcesProvider.PluginParts)
            {
                services.AddControllersWithViews().PartManager.ApplicationParts.Add(part);
            }
            //To serve user services
            foreach(var loader in pluginResourcesProvider.PluginLoaders)
            {
                loader.ConfigureUserServices(services);
            }
            services.AddSignalR();
            services.AddDbContext<HostContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IPluginManager pluginManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //To serve plugins css, js and other static resources
            foreach (var assembly in pluginResourcesProvider.Assemblies)
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new EmbeddedFileProvider(assembly, assembly.GetName().Name + ".wwwroot"),
                    //To avoid plugin resource names collisions
                    RequestPath = new PathString("/" + assembly.GetName().Name + "/wwwroot")
                });
            }

            app.UseRouting();

            foreach(var plugin in pluginManager.GetPlugins())
            {
                plugin.InitializeAsync();
            }

            app.UseEndpoints(endpoints =>
            {
                //To give the ability to a user to customize endpoints for controllers and hubs
                foreach (var pluginLoader in pluginResourcesProvider.PluginLoaders)               {
                    pluginLoader.UseEndpoints(endpoints);
                }
                //To synchronizes Web and Wpf user interfaces
                endpoints.MapHub<HostHub>("/Host");

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

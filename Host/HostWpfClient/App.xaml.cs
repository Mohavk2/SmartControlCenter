using HostWpfClient.Services;
using HostWpfClient.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfInfrastructure.CommonResources;

namespace HostWpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static HubConnection hostConnection;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IServiceCollection services = new ServiceCollection();

            await ConnectToHost();

            var provider = ConfigureServices(services).BuildServiceProvider();

            var window = provider.GetRequiredService<MainWindow>();

            window.Show();
        }

        IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HubConnection>(ProviderSettings=> {
                return hostConnection;
            });

            return services;
        }

        async Task ConnectToHost()
        {
            hostConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44371/RemoteBinding")
                .Build();

            hostConnection.Closed += async (err) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hostConnection.StartAsync();
            };

            try
            {
                await hostConnection.StartAsync();
            }
            catch (Exception)
            {
                //TODO: Add Loging
                throw;
            }
        }
    }
}

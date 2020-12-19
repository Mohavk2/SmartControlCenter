using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using TestPluginWpf.ViewModels;
using TestPluginWpf.Views;
using WpfInfrastructure;

namespace TestPluginWpf
{
    public class TestPlugin : IWpfPlugin
    {
        public string Name { get; } = "TestPlugin";

        Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        HubConnection hubConnection;

        //This method will be called from a worker thread so don't put any UI thread related logic in here or use Dispatcher!!!
        public async Task InitializeAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44371/TestNotification")
                .Build();

            hubConnection.Closed += async (err) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
            };

            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                throw new Exception($"Unable to connect to the WebHost. Looks like WebHost is not running or have a connection problem. ({ex.Message})");
            }
        }

        //This method runs right after user resources initialization is completed.Make sure that it's ready to return view or throw an exception.
        public UserControl GetView()
        {
            var uc = new MainViewModel(hubConnection);
            var view = new MainView(uc, hubConnection);
            return view;
        }
    }
}

using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPluginWeb.Services
{
    public class HubConnectionService
    {
        protected HubConnection hubConnection;
        string url { get; set; }

        public HubConnectionService(string url)
        {
            this.url = url;
        }

        public async Task ConnectToHubAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
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
                throw new Exception(
                    $"Unable to connect to the WebHost. Looks like WebHost is not running or a connection problem appeared. ({ex.Message})",
                    ex);
            }
        }
    }
}

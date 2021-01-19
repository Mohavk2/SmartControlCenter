using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestPluginWeb.Services
{
    public class HubConnectionService
    {
        protected HubConnection hubConnection;

        public HubConnectionService(string url)
        {
            hubConnection = new HubConnectionBuilder().WithUrl(url).Build();
            hubConnection.Closed += Reconnect;
        }

        protected async Task SendAsync(string method, object arg, CancellationToken cancellationToken = default)
        {
            if(hubConnection.State == HubConnectionState.Disconnected)
            {
                await ConnectToHubAsync();
            }
            await hubConnection.SendAsync(method, arg, cancellationToken);
        }

        async Task ConnectToHubAsync()
        {
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
        async Task Reconnect(Exception ex)
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await hubConnection.StartAsync();
        }
    }
}

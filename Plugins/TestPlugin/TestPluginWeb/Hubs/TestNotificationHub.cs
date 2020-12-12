using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPluginWeb.Hubs
{
    public class TestNotificationHub : Hub
    {
        public Task SendTest(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
    }
}

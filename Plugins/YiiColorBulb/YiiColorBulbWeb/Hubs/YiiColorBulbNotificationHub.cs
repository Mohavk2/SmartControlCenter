using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiiColorBulbWeb.Hubs
{
    public class YiiColorBulbNotificationHub : Hub
    {
        public Task SendTest(string message)
        {
            return Clients.Others.SendAsync("send", message);
        }
    }
}

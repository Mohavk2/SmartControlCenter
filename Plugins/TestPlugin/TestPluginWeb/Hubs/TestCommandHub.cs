using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;

namespace TestPluginWeb.Hubs
{
    public class TestCommandHub : Hub
    {
        public TestCommandHub()
        {

        }

        public Task MoveCircle(string[] parameters)
        {
            int x, y;
            try
            {
                x = int.Parse(parameters[0]);
                y = int.Parse(parameters[1]);
            }
            catch (Exception ex)
            {
                throw new Exception("Incorrect parameters for a command MoveCirlce ", ex);
            }
            return Clients.All.SendAsync("MoveCircle", x, y);
        }

        public Task RunTest2(List<object> parameters)
        {
            return Clients.All.SendAsync("RunTest2", parameters);
        }
    }
}

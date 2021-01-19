using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;

namespace TestPluginWeb.Services
{
    public class ActionExecutor : HubConnectionService
    {
        public ActionExecutor() : base("https://localhost:44371/TestCommand") { }

        public async Task Run(ActionP action)
        {
            await Task.Delay(action.Delay);

            foreach(var command in action.Commands)
            {
                await Task.Delay(command.Delay);
                await SendAsync(command.Method, command.Parameters);
            }
        }
    }
}

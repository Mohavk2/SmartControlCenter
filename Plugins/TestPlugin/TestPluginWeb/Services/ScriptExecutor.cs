using CommonInfrastructure.DTO;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;

namespace TestPluginWeb.Services
{
    public class ScriptExecutor : HubConnectionService
    {
        public ScriptExecutor() : base("https://localhost:44371/TestAction") { }

        public async Task Run(ScriptP script)
        {
            foreach(var action in script.Actions)
            {
                await Task.Delay(action.Delay);
                await hubConnection.SendAsync("Run", action);
            }
        }
    }
}
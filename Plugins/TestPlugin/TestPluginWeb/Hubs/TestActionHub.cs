using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.Models;
using TestPluginWeb.Repositories;
using TestPluginWeb.Services;

namespace TestPluginWeb.Hubs
{
    public class TestActionHub : Hub
    {
        ActionRepository actionRepository;
        ActionExecutor actionExecutor;

        public TestActionHub(ActionRepository actionRepository, ActionExecutor actionExecutor)
        {
            this.actionRepository = actionRepository;
            this.actionExecutor = actionExecutor;
        }
        public Task Get(int id)
        {
            var action = actionRepository.Get(id);
            return Clients.Caller.SendAsync("Get", action);
        }
        public Task GetAll()
        {
            var actions = actionRepository.GetAll();
            return Clients.Caller.SendAsync("GetAll", actions);
        }
        public Task Add(ActionP action)
        {
            actionRepository.Add(action);
            return Clients.All.SendAsync("Add", action);
        }
        public async Task Run(int id)
        {
            var action = actionRepository.Get(id);
            await Clients.All.SendAsync("Run", action);
            await actionExecutor.Run(action);
        }
    }
}

using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfInfrastructure.CommonResources;

namespace TestPluginWpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        HubConnection hubConnection;

        public MainViewModel(HubConnection hubConnection)
        {
            this.hubConnection = hubConnection;
        }
    }
}

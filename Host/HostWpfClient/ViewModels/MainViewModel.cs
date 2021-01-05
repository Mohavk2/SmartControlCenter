using CommonInfrastructure.DTO;
using HostWpfClient.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WpfInfrastructure;
using WpfInfrastructure.CommonResources;

namespace HostWpfClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        HubConnection hostConnection;

        public ObservableCollection<ScriptDTO> Scripts { get; set; } = new ObservableCollection<ScriptDTO>();

        public ObservableCollection<PluginWrapperVM> PluginsWrappers { get; set; } = new ObservableCollection<PluginWrapperVM>();

        public MainViewModel(HubConnection hostConnection)
        {
            this.hostConnection = hostConnection;

            var plugins = PluginLoader.LoadPlugins();

            foreach (var plugin in plugins)
            {
                PluginsWrappers.Add(new PluginWrapperVM(plugin));
            }
        }

        ScriptDTO currentScript;
        public ScriptDTO CurrentScript
        {
            get => currentScript;
            set 
            {
                currentScript = value; 
                RaisePropertyChanged(nameof(CurrentScript)); 
            } 
        }

        public ICommand RunScript
        {
            get => new UICommand(ExecuteRunScript);
        }

        async void ExecuteRunScript(object parameter)
        {
            await hostConnection.SendAsync("RunScript", "script1");
        }

        void OnScriptStatusChanged(string pluginId, string scriptId, bool status)
        {

        }

        public ICommand StopScript
        {
            get => new UICommand(ExecuteStopScript);
        }

        async void ExecuteStopScript(object parameter)
        {
            await hostConnection.SendAsync("StopScript", "script1");
        }
    }
}

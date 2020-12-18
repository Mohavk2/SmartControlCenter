using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfInfrastructure;
using WpfInfrastructure.CommonResources;

namespace HostWpfClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<PluginWrapperVM> PluginsWrappers { get; set; } = new ObservableCollection<PluginWrapperVM>();

        public MainViewModel()
        {
            var plugins = PluginLoader.LoadPlugins();

            foreach(var plugin in plugins)
            {
                PluginsWrappers.Add(new PluginWrapperVM(plugin));
            }
        }
    }
}

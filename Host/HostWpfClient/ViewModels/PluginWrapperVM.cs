using HostWpfClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfInfrastructure;
using WpfInfrastructure.CommonResources;

namespace HostWpfClient.ViewModels
{
    public class PluginWrapperVM : BaseViewModel
    {
        Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        IWpfPlugin plugin { get; set; }

        public string Name => plugin.GetName();

        UserControl view;
        public UserControl View
        {
            get => view;
            set
            {
                view = value;
                RaisePropertyChanged(nameof(View));
            }
        }

        public PluginWrapperVM(IWpfPlugin plugin)
        {
            this.plugin = plugin;

            View = new PluginViewAwaiter();

            Task.Run(() => LoadPluginView());
        }

        void LoadPluginView()
        {
            string errorMessage = null;
            try
            {
                plugin.InitializeAsync().Wait();
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                //TODO: Add logging
            }

            dispatcher.Invoke(() => 
            {
                View = (errorMessage == null) ? plugin.GetView() : new SomethingWentWrong(errorMessage);
            });
        }
    }
}

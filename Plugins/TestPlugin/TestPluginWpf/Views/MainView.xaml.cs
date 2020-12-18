using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestPluginWpf.ViewModels;

namespace TestPluginWpf.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        HubConnection hubConnection;

        public MainView(MainViewModel uc, HubConnection hubConnection)
        {
            InitializeComponent();
            DataContext = uc;
            this.hubConnection = hubConnection;

            hubConnection.On<string>("Send", (message) =>
            {
                this.Dispatcher.Invoke(()=>TestOutput.Text = message);
            });

            TestOutput.Text = hubConnection.State.ToString();
        }

        private async void TestSignalRButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await hubConnection.SendAsync("SendTest", TestInput.Text);
            }
            catch (Exception ex)
            {
                TestOutput.Text = ex.Message;
            }
        }
    }
}

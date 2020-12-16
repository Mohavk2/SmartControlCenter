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

namespace HostWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static HubConnection hubConnection;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44371/TestNotification")
                .Build();

            hubConnection.Closed += async (err) =>
            {
                TestInput.Text = "SignalR disconnected";
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
                TestOutput.Text = "SignalR connected";
            };

            try
            {
                await hubConnection.StartAsync();
                TestOutput.Text = "SignalR connected";
            }
            catch (Exception ex)
            {
                TestOutput.Text = ex.Message;
            }

            hubConnection.On<string>("Send", (message) =>
            {
                TestOutput.Text = message;
            });
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

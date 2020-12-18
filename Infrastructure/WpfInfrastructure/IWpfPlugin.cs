using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfInfrastructure
{

    public interface IWpfPlugin
    {
        public string Name { get; }

        /// <summary>
        /// This method will be called from a worker thread so don't put any UI thread related logic here or use Dispatcher!!!
        /// </summary>
        /// <returns></returns>
        public Task InitializeAsync();

        public UserControl GetView();
    }
}

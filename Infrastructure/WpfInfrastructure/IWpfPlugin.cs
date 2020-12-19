using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfInfrastructure
{

    public interface IWpfPlugin
    {
        public string GetName();
        /// <summary>
        /// This method will be called from a worker thread so don't put any UI thread related logic here or use Dispatcher!!!
        /// </summary>
        /// <returns></returns>
        public Task InitializeAsync();
        /// <summary>
        /// This method runs right after user resources initialization is completed. Make sure that it's ready to return view or throw an exception.
        /// </summary>
        /// <returns></returns>
        public UserControl GetView();
    }
}

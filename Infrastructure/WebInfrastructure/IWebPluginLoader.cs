using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebInfrastructure
{
    public interface IWebPluginLoader
    {
        public void ConfigureUserServices(IServiceCollection services);
    }
}

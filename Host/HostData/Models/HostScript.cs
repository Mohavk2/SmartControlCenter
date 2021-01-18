using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostData.Models
{
    public class HostScript
    {
        public int Duration { get; set; } = 0;
        public Dictionary<string, List<PluginScript>> PluginsScripts { get; set; } = new Dictionary<string, List<PluginScript>>();
    }
}

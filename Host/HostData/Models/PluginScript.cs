using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostData.Models
{

    public class PluginScript
    {
        static int count = 0;

        public int Id { get; set; }
        public string PluginName { get; set; }
        public int InnerId { get; set; }
        public string Name { get; set; }
        public int BeginsAt { get; set; }
        public int EndsAt { get; set; }
        public int Duration => EndsAt - BeginsAt;

        public PluginScript()
        {
            Id = count++;
        }
    }
}

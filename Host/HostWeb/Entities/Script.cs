using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Entities
{

    public class Script
    {
        static int count = 0;

        public int Id { get; set; }
        public int PluginId { get; set; }
        public int InnerId { get; set; }
        public string Name { get; set; }

        public Script()
        {
            Id = count++;
        }
    }
}

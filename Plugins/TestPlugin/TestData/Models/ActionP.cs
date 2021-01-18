using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestData.Models
{
    public class ActionP
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Repeat { get; set; } = 1;
        public int Delay { get; set; } = 0;
        public List<CommandP> Commands { get; set; } = new List<CommandP>();
    }
}

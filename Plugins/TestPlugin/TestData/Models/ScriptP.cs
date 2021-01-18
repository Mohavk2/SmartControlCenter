using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestData.Models
{
    public class ScriptP
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; } = 0;
        public List<ActionP> Actions { get; set; } = new List<ActionP>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TestData.Models
{
    public class CommandP
    {
        public int Delay { get; set; }
        public string Method { get; set; }
        public string[] Parameters { get; set; }
    }
}

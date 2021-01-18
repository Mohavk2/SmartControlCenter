using HostData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostWeb.Interfaces
{
    public interface IScriptProcessor
    {
        public Task RunScriptAsync(HostScript hostScript);
        public void StopRunningScript();
        bool isScriptRunning { get; }
    }
}

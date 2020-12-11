using Microsoft.AspNetCore.Routing;
using System;

namespace Infrastructure
{
    public interface IPlugin
    {
        public string Name { get; }

        public void UseEndpoints(IEndpointRouteBuilder endpoints);
    }
}

using Microsoft.AspNetCore.Routing;
using System;

namespace WebInfrastructure
{
    public interface IWebPlugin
    {
        public string Name { get; }

        public void UseEndpoints(IEndpointRouteBuilder endpoints);
    }
}

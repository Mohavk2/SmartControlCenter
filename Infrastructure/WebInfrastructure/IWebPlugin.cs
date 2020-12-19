using Microsoft.AspNetCore.Routing;
using System;

namespace WebInfrastructure
{
    public interface IWebPlugin
    {
        public string GetName();

        public void UseEndpoints(IEndpointRouteBuilder endpoints);
    }
}

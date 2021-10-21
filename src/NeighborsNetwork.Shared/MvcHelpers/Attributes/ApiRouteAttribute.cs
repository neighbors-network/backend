using Microsoft.AspNetCore.Mvc;

namespace NeighborsNetwork.Shared.MvcHelpers.Attributes
{
    public class ApiRouteAttribute : RouteAttribute
    {
        public ApiRouteAttribute(string template) : base($"~/api/{template}")
        {
        }

        public ApiRouteAttribute() : this("[controller]")
        {
        }
    }
}
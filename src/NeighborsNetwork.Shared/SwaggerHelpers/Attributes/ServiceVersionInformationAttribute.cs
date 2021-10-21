using System;

namespace NeighborsNetwork.Shared.SwaggerHelpers.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ServiceVersionInformationAttribute : Attribute
    {
        public string Slug { get; init; }
        public string? ServiceName { get; init; }
        public string Version { get; init; }

        public ServiceVersionInformationAttribute(string slug)
        {
            Slug = slug;
            Version = "v1";
        }
    }
}

using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NeighborsNetwork.Shared.SwaggerHelpers.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace NeighborsNetwork.Shared.SwaggerHelpers
{
    public static class SwaggerHelperExtensions
    {
        public static void AddServicesSwaggerGenOptions(this SwaggerGenOptions options, params Assembly[] assemblies)
        {
            foreach (var serviceVersionInformation in assemblies.Select(GetAttributeFromAssembly))
            {
                options.SwaggerDoc(serviceVersionInformation.Slug, new OpenApiInfo()
                {
                    Title = serviceVersionInformation.ServiceName,
                    Version = serviceVersionInformation.Version,
                });
            }
        }

        public static void RegisterServicesEndpoints(this SwaggerUIOptions options, params Assembly[] assemblies)
        {
            foreach (var serviceVersionInformation in assemblies.Select(GetAttributeFromAssembly))
            {
                options.SwaggerEndpoint(
                    $"/swagger/{serviceVersionInformation.Slug}/swagger.json",
                    $"{serviceVersionInformation.ServiceName} {serviceVersionInformation.Version}"
                );
            }
        }

        private static ServiceVersionInformationAttribute GetAttributeFromAssembly(Assembly asm)
        {
            var identifier = asm.GetTypes().Single(type => type.IsInterface && type.IsAssignableTo(typeof(IServiceIdentifier)));

            var attr = identifier.GetCustomAttribute<ServiceVersionInformationAttribute>();
            var asmName = asm.GetName().Name ?? $"Service {Guid.NewGuid()}";

            attr ??= new ServiceVersionInformationAttribute(asmName.Replace('.', '-'));

            if (attr.ServiceName == default)
            {
                attr = new ServiceVersionInformationAttribute(attr.Slug)
                {
                    ServiceName = asmName,
                    Version = attr.Version
                };
            }

            return attr;
        }
    }
}

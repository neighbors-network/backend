using JetBrains.Annotations;
using NeighborsNetwork.Shared;
using NeighborsNetwork.Shared.SwaggerHelpers.Attributes;

namespace NeighborsNetwork.Users.Service
{
    [UsedImplicitly]
    [ServiceVersionInformation(Constants.ServiceSlug, Version = Constants.ServiceVersion)]
    public interface IUsersServiceIdentifier : IServiceIdentifier
    {
    }
}

using MediatR;
using NeighborsNetwork.Users.Service.Processing.Responses;

namespace NeighborsNetwork.Users.Service.Processing.Requests
{
    public class GetUsersRequest : IRequest<GetUsersResponse>
    {
        public int Page { get; init; }
        public int ItemsCount { get; init; }

        public bool IncludeDisabled { get; init; }
    }
}

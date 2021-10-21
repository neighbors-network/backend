using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NeighborsNetwork.Users.Db;
using NeighborsNetwork.Users.Service.Processing.Requests;
using NeighborsNetwork.Users.Service.Processing.Responses;

namespace NeighborsNetwork.Users.Service.Processing.Handlers
{
    [UsedImplicitly]
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly UsersContext _ctx;
        private readonly IMapper _mapper;

        public GetUsersHandler(UsersContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var usersRequest = _ctx.Users
                .Where(u => !u.Disabled)
                .OrderBy(u => u.CreatedAt)
                .AsNoTracking();

            var skipCount = GetSkipCount(request);

            var users = await usersRequest
                .Skip(skipCount)
                .Take(request.ItemsCount)
                .ToListAsync(cancellationToken);

            return new GetUsersResponse
            {
                Users = _mapper.Map<IEnumerable<UserDto>>(users),
                Count = await usersRequest.CountAsync(cancellationToken)
            };
        }

        private static int GetSkipCount(GetUsersRequest request)
        {
            return request.Page switch
            {
                >= 1 => request.ItemsCount * (request.Page - 1),
                _ => 0
            };
        }
    }
}

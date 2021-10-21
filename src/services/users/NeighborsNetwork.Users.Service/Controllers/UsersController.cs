using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeighborsNetwork.Shared.MvcHelpers;
using NeighborsNetwork.Shared.MvcHelpers.Attributes;
using NeighborsNetwork.Users.Service.Attributes;
using NeighborsNetwork.Users.Service.Processing.Requests;
using NeighborsNetwork.Users.Service.Processing.Responses;
using OneOf;
using OneOf.Types;

namespace NeighborsNetwork.Users.Service.Controllers
{
    [ApiController, ApiRoute, UsersApiExplorerSettings]
    public class UsersController : CrudControllerBase<UserDto>
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task<OneOf<IEntityPageListResponse<UserDto>, Error<Exception>>> GetItemsList(int page, int count)
        {
            try
            {
                return await _mediator.Send(new GetUsersRequest {Page = page, ItemsCount = count, IncludeDisabled = false});
            }
            catch (Exception e)
            {
                return new Error<Exception>(e);
            }
        }
    }
}

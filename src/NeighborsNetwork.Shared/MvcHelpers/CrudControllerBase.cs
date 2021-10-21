using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;

namespace NeighborsNetwork.Shared.MvcHelpers
{
    public interface IEntityPageListResponse<out TEntity>
    {
        [Required]
        public IEnumerable<TEntity> Users { get; }

        [Required]
        public int Count { get; }
    }

    public abstract class CrudControllerBase<T> : ControllerBase
    {
        [HttpGet("list")]
        public async Task<ActionResult<IEntityPageListResponse<T>>> ListItems(int page = 1, int count = 10)
        {
            return (await GetItemsList(page, count)).Match<ActionResult>(
                Ok,
                e => BadRequest(e.Value?.Message)
            );
        }

        protected abstract Task<OneOf<IEntityPageListResponse<T>, Error<Exception>>> GetItemsList(int page, int count);
    }
}

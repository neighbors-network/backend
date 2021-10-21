using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NeighborsNetwork.Shared.MvcHelpers;

namespace NeighborsNetwork.Users.Service.Processing.Responses
{
    public class UserDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        public string? Middlename { get; set; }

        [Required]
        public bool Disabled { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
    }

    public class GetUsersResponse : IEntityPageListResponse<UserDto>
    {
        public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();
        public int Count { get; set; } = 0;
    }
}

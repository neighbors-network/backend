using System;
using System.ComponentModel.DataAnnotations;

namespace NeighborsNetwork.Users.Db.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        public string? Middlename { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public bool Disabled { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }
    }
}

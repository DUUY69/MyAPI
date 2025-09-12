using MyAPI.Repositories.Infrastructure;
using System;
using System.Collections.Generic;

namespace MyAPI.Repositories.Entities
{
    public partial class User : IEntityBase
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        // Add navigation properties here
        // Example:
        // public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

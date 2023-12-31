﻿namespace SalesSystem.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public int? RoleId { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        public virtual Role? RoleNavigationId { get; set; }
    }

}
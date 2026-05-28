using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Enums;

namespace AccessManager.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BadgeNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public AccessLevel AccessLevel { get; set; }
    }
}

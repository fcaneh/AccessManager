using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Enums;

namespace AccessManager.Application.Features.Users.CreateUser
{
    public record CreateUserCommand
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string BadgeNumber { get; init; } = string.Empty;
        public AccessLevel AccessLevel { get; init; }
    }
}

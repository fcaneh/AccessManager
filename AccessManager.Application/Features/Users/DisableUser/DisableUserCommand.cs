using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManager.Application.Features.Users.DisableUser
{
    public record DisableUserCommand
    {
        public string BadgeNumber { get; init; }
    }
}

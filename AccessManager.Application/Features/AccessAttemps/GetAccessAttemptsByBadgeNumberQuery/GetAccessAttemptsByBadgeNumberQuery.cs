using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManager.Application.Features.AccessAttemps.GetAccessAttemptsByBadgeNumberQuery
{
    public record GetAccessAttemptsByBadgeNumberQuery
    {
        public string BadgeNumber { get; init; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManager.Application.Features.Access.CheckAccess
{
    public record CheckAccessQuery
    {
        public string BadgeNumber { get; init; } = string.Empty;
        public Guid ZoneId { get; init; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AccessManager.Domain.Entities;

namespace AccessManager.Application.Features.AccessAttemps.GetAccessAttemptsByBadgeNumberQuery
{
    public record GetAccessAttemptsByBadgeNumberResponse
    {
        public IEnumerable<AccessAttempt> AccessAttempts { get; init; }
            = Enumerable.Empty<AccessAttempt>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManager.Application.Features.Statistics
{
    public record GetStatisticsResponse
    {
        public int UsersCount { get; init; }
        public int ActiveUsersCount { get; init; }
        public int InactiveUsersCount { get; init; }
        public int TotalAccessAttempts { get; init; }
        public int GrantedAccessAttempts { get; init; }
        public int DeniedAccessAttempts { get; init; }
        public string MostUsedZone { get; init; } = string.Empty;
    }
}

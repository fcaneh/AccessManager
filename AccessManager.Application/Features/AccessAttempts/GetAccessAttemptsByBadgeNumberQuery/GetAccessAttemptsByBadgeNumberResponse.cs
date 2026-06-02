using AccessManager.Domain.Entities;

namespace AccessManager.Application.Features.AccessAttempts.GetAccessAttemptsByBadgeNumber
{
    public record GetAccessAttemptsByBadgeNumberResponse
    {
        public IEnumerable<AccessAttempt> AccessAttempts { get; init; }
            = Enumerable.Empty<AccessAttempt>();
    }
}

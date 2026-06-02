using AccessManager.Domain.Entities;

namespace AccessManager.Application.Features.AccessAttempts.GetAllAccessAttempts
{
    public record GetAllAccessAttemptsResponse
    {
        public IEnumerable<AccessAttempt> AccessAttempts { get; init; } = new List<AccessAttempt>();
    }
}

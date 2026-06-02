using AccessManager.Domain.Entities;

namespace AccessManager.Application.Features.Users.GetUserByBadgeNumber
{
    public record GetUserByBadgeNumberResponse
    {
        public User? User { get; init; }
    }
}

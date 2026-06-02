namespace AccessManager.Application.Features.Users.GetUserByBadgeNumber
{
    public record GetUserByBadgeNumberQuery
    {
        public string BadgeNumber { get; init; } = string.Empty;
    }
}

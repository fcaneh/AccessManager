namespace AccessManager.Application.Features.AccessAttempts.GetAccessAttemptsByBadgeNumber
{
    public record GetAccessAttemptsByBadgeNumberQuery
    {
        public string BadgeNumber { get; init; } = string.Empty;
    }
}

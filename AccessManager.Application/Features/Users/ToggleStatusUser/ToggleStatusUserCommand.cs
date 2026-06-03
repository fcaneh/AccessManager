namespace AccessManager.Application.Features.Users.ToggleStatusUser
{
    public record ToggleStatusUserCommand
    {
        public string BadgeNumber { get; init; } = string.Empty;
    }
}

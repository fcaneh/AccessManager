namespace AccessManager.Application.Features.Users.ToggleStatusUser
{
    public record ToggleStatusUserResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}

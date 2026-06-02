namespace AccessManager.Application.Features.Users.DisableUser
{
    public record DisableUserResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}

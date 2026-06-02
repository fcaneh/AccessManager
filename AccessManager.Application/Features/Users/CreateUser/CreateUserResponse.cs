namespace AccessManager.Application.Features.Users.CreateUser
{
    public record CreateUserResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}

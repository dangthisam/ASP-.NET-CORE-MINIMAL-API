

namespace bookapi_minimal.Contracts
{

    public record AuthenticateUserRequest
    {
        public string Username { get; init; }
        public string PasswordHash { get; init; }
    }


}
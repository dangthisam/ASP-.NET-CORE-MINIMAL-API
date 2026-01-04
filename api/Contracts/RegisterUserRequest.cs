namespace bookapi_minimal.Contracts
{

    public record RegisterUserRequest
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }
        public string Address { get; init; }

    }

}
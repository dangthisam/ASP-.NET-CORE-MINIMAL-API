

namespace bookapi_minimal.Contracts
{

    public record UserResponse
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
        public string Role { get; init; }
    }

}
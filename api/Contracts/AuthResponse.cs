
namespace bookapi_minimal.Contracts
{

    public record AuthResponse
    {
        public string Token { get; init; }
        public UserResponse User { get; init; }
    }

}

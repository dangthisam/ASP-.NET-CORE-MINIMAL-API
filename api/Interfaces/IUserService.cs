using bookapi_minimal.Contracts;
using bookapi_minimal.Models;

namespace bookapi_minimal.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest);
        Task<AuthResponse> AuthenticateUserAsync(AuthenticateUserRequest authenticateUserRequest);

      
    }
}
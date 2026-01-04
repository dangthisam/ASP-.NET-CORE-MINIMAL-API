using bookapi_minimal.AppContext;
using bookapi_minimal.Contracts;
using bookapi_minimal.Interfaces;
using bookapi_minimal.Models;
using Microsoft.EntityFrameworkCore;

namespace bookapi_minimal.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly JwtService _jwtService;

        public UserService(ApplicationContext context, ILogger<UserService> logger, JwtService jwtService)
        {
            _context = context;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> AuthenticateUserAsync(AuthenticateUserRequest authenticateUserRequest)
        {
         try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == authenticateUserRequest.Username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(authenticateUserRequest.PasswordHash, user.PasswordHash))
                {
                    _logger.LogWarning($"Invalid authentication attempt for username '{authenticateUserRequest.Username}'.");
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                var token = _jwtService.GenerateToken(user.Username, user.Role);

                _logger.LogInformation($"User with ID {user.Id} authenticated successfully.");

                var authResponse = new AuthResponse
                {
                    Token = token,
                    User = new UserResponse
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        Address = user.Address,
                        Role = user.Role
                    }
                };

                return authResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AuthenticateUserAsync)}: {ex.Message}");
                throw;
            }
        }

       
        public async Task<UserResponse> RegisterUserAsync(RegisterUserRequest registerUserRequest)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == registerUserRequest.Username || u.Email == registerUserRequest.Email);

                if (existingUser != null)
                {
                    _logger.LogWarning($"User with username '{registerUserRequest.Username}' or email '{registerUserRequest.Email}' already exists.");
                    throw new InvalidOperationException("Username or email already exists.");
                }

                // Hash password using BCrypt
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.PasswordHash);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = registerUserRequest.Username,
                    Email = registerUserRequest.Email,
                    PasswordHash = passwordHash,
                    Address = registerUserRequest.Address,
                    Role = "User"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"User with ID {user.Id} registered successfully.");

                return new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Address = user.Address,
                    Role = user.Role
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(RegisterUserAsync)}: {ex.Message}");
                throw;
            }
        }
    }
}

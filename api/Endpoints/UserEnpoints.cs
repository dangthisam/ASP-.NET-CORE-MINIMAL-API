using bookapi_minimal.Interfaces;
using Microsoft.AspNetCore.Builder;
using bookapi_minimal.Contracts;


namespace bookapi_minimal.Endpoints
{
    public static class UserEnpoints
    {
 public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
       



        app.MapPost("/register", async (RegisterUserRequest registerUserRequest, IUserService userService) =>
        {
            var userResponse = await userService.RegisterUserAsync(registerUserRequest);
            return Results.Created($"/users/{userResponse.Id}", userResponse);
        });



        app.MapPost("/authenticate", async (AuthenticateUserRequest authenticateUserRequest, IUserService userService) =>
        {
            var authResponse = await userService.AuthenticateUserAsync(authenticateUserRequest);
            return Results.Ok(authResponse);
        });

        return app;
    }
    }  
}
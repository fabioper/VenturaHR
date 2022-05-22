using Users.Api.Controllers;
using Users.Api.DTOs.Requests;
using Users.Api.DTOs.Responses;

namespace Users.Api.Services.Contracts;

public interface IUserService
{
    Task CreateUser(CreateUserRequest request);
    Task<TokenResponse> Authenticate(LoginRequest request);
    Task<TokenResponse> RefreshToken(RefreshTokenRequest request);
}
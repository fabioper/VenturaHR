using Users.Application.DTOs.Requests;
using Users.Application.DTOs.Responses;

namespace Users.Application.Services.Contracts;

public interface IUserService
{
    Task CreateUser(CreateUserRequest request);
    Task<TokenResponse> Authenticate(LoginRequest request);
    Task<TokenResponse> RefreshToken(RefreshTokenRequest request);
    Task<UserProfileResponse> GetUserProfile(string userId);
}
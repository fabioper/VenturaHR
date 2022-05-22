using Users.Api.DTOs.Requests;
using Users.Api.DTOs.Responses;

namespace Users.Api.Services.Contracts;

public interface IUserService
{
    Task CreateUser(CreateUserRequest request);
    Task<UserProfileResponse> FindUserOfId(string userId);
}
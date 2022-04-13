using Users.Services.Concretes.Models;

namespace Users.Services.Contracts;

public interface IKeycloakClient
{
    Task<UserRepresentation?> RegisterUser(UserRepresentation user);
    Task<UserRepresentation?> FindUser(string username);
    Task<List<UserRepresentation>> GetUsers(Dictionary<string, string?>? query);
    Task DeleteUser(string username);
}
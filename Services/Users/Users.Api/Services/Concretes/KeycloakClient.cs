using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Users.Api.ConfigOptions;
using Users.Api.Services.Concretes.Models;
using Users.Api.Services.Contracts;

namespace Users.Api.Services.Concretes;

public class KeycloakClient : IKeycloakClient
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;

    private string TokenEndpoint => "realms/master/protocol/openid-connect/token";
    private string UsersEndpoint => $"admin/realms/{_keycloakOptions.Realm}/users";

    public KeycloakClient(HttpClient httpClient, KeycloakOptions keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions;
        
        _httpClient.BaseAddress = new Uri(_keycloakOptions.BaseUri);
    }

    public async Task<List<UserRepresentation>> GetUsers(Dictionary<string, string?>? query)
    {
        var client = await GetAuthorizedClient();
        var endpoint = query != null
            ? QueryHelpers.AddQueryString(UsersEndpoint, query) 
            : UsersEndpoint;

        var response = await client.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<List<UserRepresentation>>();
        return users ?? throw new InvalidOperationException();
    }

    public async Task DeleteUser(string userId)
    {
        var client = await GetAuthorizedClient();
        var response = await client.DeleteAsync($"{UsersEndpoint}/{userId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<UserRepresentation?> FindUser(string username)
    {
        var users = await GetUsers(new()
        {
            { "exact", true.ToString().ToLower() },
            { "username", username }
        });

        return users.FirstOrDefault();
    }

    public async Task<UserRepresentation?> RegisterUser(UserRepresentation user)
    {
        var client = await GetAuthorizedClient();
        var response = await client.PostAsJsonAsync(UsersEndpoint, user);
        response.EnsureSuccessStatusCode();
        return await FindUser(user.Email);
    }

    private async Task<HttpClient> GetAuthorizedClient()
    {
        var token = await GetAuthToken();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.Token);
        return _httpClient;
    }

    private async Task<AccessTokenRepresentation> GetAuthToken()
    {
        var response = await _httpClient.PostAsync(TokenEndpoint,
            new FormUrlEncodedContent(GetClientCredentials()));

        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadFromJsonAsync<AccessTokenRepresentation>();
        return token ?? throw new InvalidOperationException();
    }

    private Dictionary<string, string> GetClientCredentials() => new()
    {
        { "grant_type", "client_credentials" },
        { "client_id", _keycloakOptions.ClientId },
        { "client_secret", _keycloakOptions.ClientSecret }
    };
}
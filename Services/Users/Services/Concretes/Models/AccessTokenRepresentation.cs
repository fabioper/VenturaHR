using System.Text.Json.Serialization;

namespace Users.Services.Concretes.Models;

public class AccessTokenRepresentation
{
    [JsonPropertyName("access_token")]
    public string Token { get; set; }
}
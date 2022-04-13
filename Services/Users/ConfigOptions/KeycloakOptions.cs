namespace Users.ConfigOptions;

public class KeycloakOptions
{
    public const string Keycloack = "Keycloak";

    public string BaseUri { get; set; }
    public string Realm { get; set; }
    public string ClientSecret { get; set; }
    public string ClientId { get; set; }
}
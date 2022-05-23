namespace Users.Application.Options;

#nullable disable

public class TokenSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public int ExpirationInMinutes { get; set; }
}
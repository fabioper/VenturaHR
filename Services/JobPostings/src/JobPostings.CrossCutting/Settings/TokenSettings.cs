namespace JobPostings.CrossCutting.Settings;

#nullable disable

public class TokenSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
}
namespace Users.Services.Concretes.Models;

public class UserRepresentation
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string[] Groups { get; set; }
    public bool Enabled { get; set; }
    public bool EmailVerified { get; set; }

    public UserRepresentation(string email, string firstName, string[] groups)
    {
        Email = email;
        Username = email;
        FirstName = firstName;
        Enabled = true;
        EmailVerified = true;
        Groups = groups;
    }

    public UserRepresentation()
    {
    }
}
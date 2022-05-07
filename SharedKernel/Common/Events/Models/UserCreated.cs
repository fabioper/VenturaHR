namespace Common.Events.Models;

public class UserCreated
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public List<string> Role { get; set; }
}
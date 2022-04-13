namespace Common.EventBus.Models;

public record CompanyRegistered(string AccountId)
{
    public string AccountId { get; set; } = AccountId;
}
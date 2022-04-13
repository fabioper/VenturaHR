namespace Common.EventBus.Models;

public record ApplicantRegistered(string AccountId)
{
    public string AccountId { get; set; } = AccountId;
}
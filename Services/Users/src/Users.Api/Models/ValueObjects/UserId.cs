using Common.Abstractions;

namespace Users.Api.Models.ValueObjects;

public record UserId(string Value) : EntityId;
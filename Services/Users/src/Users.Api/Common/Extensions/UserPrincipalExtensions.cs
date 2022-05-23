using System.Security.Claims;

namespace Users.Api.Common.Extensions;

public static class UserPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(ClaimTypes.NameIdentifier);
}
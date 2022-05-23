using System.Security.Claims;

namespace JobPostings.Api.Extensions.Principal;

public static class UserPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(ClaimTypes.NameIdentifier);
}
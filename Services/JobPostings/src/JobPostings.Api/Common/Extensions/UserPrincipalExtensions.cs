using System.Security.Claims;

namespace JobPostings.Api.Common.Extensions;

public static class UserPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(ClaimTypes.NameIdentifier);
}
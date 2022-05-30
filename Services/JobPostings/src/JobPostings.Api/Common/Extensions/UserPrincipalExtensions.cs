using System.Security.Claims;

namespace JobPostings.Api.Common.Extensions;

public static class UserPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal principal) =>
        Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
}
using System.Security.Claims;

namespace LibraryMS.Api.Common.Extensions;

public static class ClaimExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("User not found or not logged in.");

        if (!int.TryParse(userClaim, out int loggedInUserId))
        {
            throw new UnauthorizedAccessException("Invalid user identifier in token");
        }

        return loggedInUserId;
    }

    public static string GetUserRole(this ClaimsPrincipal user)
    {
        var userClaim = user.FindFirstValue(ClaimTypes.Role)
            ?? throw new UnauthorizedAccessException("User not found or not logged in.");

        return userClaim;
    }

    public static string GetUserName(this ClaimsPrincipal user)
    {
        var userClaim = user.Identity?.Name
        ?? throw new UnauthorizedAccessException("User not found or not logged in.");

        return userClaim;
    }

    public static string? GetClientId(this ClaimsPrincipal user)
    {
        var clientIdClaim = user.FindFirst("ClientId")?.Value;
        return clientIdClaim;
    }
}
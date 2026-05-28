using System.Security.Claims;

namespace LibraryMS.Api.Common.Extensions;

public static class ClaimExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("User not found or not logged in.");

        if(!int.TryParse(userClaim, out int loggedInUserId))
        {
            throw new UnauthorizedAccessException("Invalid user identifier in token");
        }

        return loggedInUserId;
    }
}
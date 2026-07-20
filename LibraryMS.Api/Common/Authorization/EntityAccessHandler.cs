using LibraryMS.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Api.Common.Authorization;

public class EntityAccessHandler
    : AuthorizationHandler<EntityAccessRequirement, int>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext ctx,
        EntityAccessRequirement requirement,
        int clientId)
    {
        var role = ctx.User.GetUserRole();
        if (role == "Admin" || role == "Employee")
        {
            ctx.Succeed(requirement);
            return Task.CompletedTask;
        }

        var clientIdClaim = ctx.User.GetClientId();
        if (clientIdClaim is not null && int.TryParse(clientIdClaim, out int result) && result == clientId)
            ctx.Succeed(requirement);
        else
            ctx.Fail();

        return Task.CompletedTask;
    }
}
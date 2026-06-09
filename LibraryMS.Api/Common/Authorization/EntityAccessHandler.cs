using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Api.Common.Authorization;

public class EntityAccessHandler(IAppDbContext context, IIdentityUser identityUser)
    : AuthorizationHandler<EntityAccessRequirement, int>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext ctx, EntityAccessRequirement requirement, int clientId)
    {
        var userId = ctx.User.GetUserId();

        var isStaff = ctx.User.IsInRole("Admin") || ctx.User.IsInRole("Employee");
        if (isStaff)
        {
            ctx.Succeed(requirement);
            return;
        }

        var client = await context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == clientId);

        if (client is null)
        {
            ctx.Fail();
            return;
        }

        var userPersonId = await identityUser.GetPersonIdByUserIdAsync(userId);

        if (userPersonId == client.PersonId)
            ctx.Succeed(requirement);
        else
            ctx.Fail();
    }
}
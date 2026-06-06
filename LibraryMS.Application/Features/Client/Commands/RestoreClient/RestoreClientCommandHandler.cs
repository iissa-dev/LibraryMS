namespace LibraryMS.Application.Features.Client.Commands.RestoreClient;

public sealed class RestoreClientCommandHandler(IAppDbContext context, IIdentityUser identityUser)
    : IRequestHandler<RestoreClientCommand, Result>
{
    public async Task<Result> Handle(RestoreClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
            // UnDelete Client
            var client = await context.Clients
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(c => c.Id == request.ClientId && c.IsDeleted, cancellationToken);

            if (client is null) return Result.Failure("Client not found");

            var userResult = await identityUser.CurrentUserByIdAsync(request.UserId);
            if (userResult.IsFailure)
            {
                return Result.Failure("User not found");
            }

            var user = userResult.Data;

            if (user?.PersonId != client.PersonId)
            {
                return Result.Failure("Security Alert: Client and User do not belong to the same person.");
            }
            client.UnDelete();

            // UnDeleteUser
            var restoreUser = await identityUser.RestoreUserAsync(request.UserId);
            if (restoreUser.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(restoreUser.Error);
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
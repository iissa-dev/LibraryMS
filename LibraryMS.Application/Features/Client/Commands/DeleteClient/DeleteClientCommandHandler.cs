namespace LibraryMS.Application.Features.Client.Commands.DeleteClient;

public sealed class DeleteClientCommandHandler(IAppDbContext context, IIdentityUser identityUser)
    : IRequestHandler<DeleteClientCommand, Result>
{
    public async Task<Result> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
            var client = await context.Clients
            .SingleOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

            if (client is null) return Result.Failure("Client not found");

            if (client.IsDeleted)
                return Result.Failure("User already deleted");

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

            context.Clients.Remove(client);

            // Soft delete
            var DeleteUser = await identityUser.DeleteUserAsync(request.UserId);
            if (DeleteUser.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(DeleteUser.Error);
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
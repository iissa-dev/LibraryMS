namespace LibraryMS.Application.Features.Client.Commands.DeleteClient;

public sealed class DeleteClientCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser)
    : IRequestHandler<DeleteClientCommand, Result>
{
    public async Task<Result> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var client = await unitOfWork.Clients.GetClientByUserId(request.UserId);
            if (client is null) return Result.Failure("User not found");

            if (client.IsDeleted)
                return Result.Failure("User already Deleted");

            unitOfWork.Clients.Delete(client);

            // Soft delete
            var user = await identityUser.DeleteUserAsync(request.UserId);
            if (user.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(user.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
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
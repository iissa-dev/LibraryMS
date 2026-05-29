using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Client.Commands.RestoreClient;

public sealed class RestoreClientCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser)
    : IRequestHandler<RestoreClientCommand, Result>
{
    public async Task<Result> Handle(RestoreClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            // UnDelete Client
            var client = await unitOfWork.Clients.GetDeletedClientByUserIdAsync(request.UserId);
            if(client is null) return Result.Failure("Client not found");

            client.UnDelete();
            // UnDeleteUser

            var userResult = await identityUser.RestoreUserAsync(request.UserId);
            if(userResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(userResult.Error);
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
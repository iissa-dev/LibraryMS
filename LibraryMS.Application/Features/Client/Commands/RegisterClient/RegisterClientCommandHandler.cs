namespace LibraryMS.Application.Features.Client.Commands.RegisterClient;

public sealed class RegisterClientCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser)
    : IRequestHandler<RegisterClientCommand, Result<int>>
{
    public async Task<Result<int>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            if (await unitOfWork.Clients.ExistsAsync(c => c.LibraryCardNumber == request.LibraryCardNumber))
            return Result<int>.Failure("This Library Card Number is already assigned to another client.");
            
            var userResult = await identityUser.CreateUserAsync(request.Email, request.Password, request.UserName,
                request.PhoneNumber, request.FirstName, request.LastName, request.Address, request.CountryId, request.BirthDate);

            if (userResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(userResult.Error);
            }

            var client = new Domain.Entities.Client
            {
                UserId = userResult.Data,
                LibraryCardNumber = request.LibraryCardNumber,
            };
            unitOfWork.Clients.Add(client);

            var roleResult = await identityUser.AddUserToRoleAsync(request.UserName, Roles.Client);

            if (roleResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(roleResult.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result<int>.Success(client.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
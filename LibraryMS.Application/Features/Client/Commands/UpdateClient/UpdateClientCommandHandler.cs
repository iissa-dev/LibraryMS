using LibraryMS.Application.DTOs.UserDto;

namespace LibraryMS.Application.Features.Client.Commands.UpdateClient;

public class UpdateClientCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser) : IRequestHandler<UpdateClientCommand, Result>
{
    public async Task<Result> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            // Update Client info 
            var client = await unitOfWork.Clients.GetClientByUserId(request.UserId);
            if (client is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure("Client does not exists");
            }

            client.LibraryCardNumber = request.LibraryCardNumber;

            // Update User info
            var userData = new UpdateUserInfoDto
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UserName = request.UserName,
                ImageUrl = request.ImageUrl,
                DateOfBirth = request.DateOfBirth,
                CountryId = request.CountryId
            };

            var updateUser = await identityUser.UpdateUserInfoAsync(userData);

            if (updateUser.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(updateUser.Error);
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
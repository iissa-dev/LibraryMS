using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Application.Mapper;
using LibraryMS.Application.Result;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using MediatR;

namespace LibraryMS.Application.Features.Client.Commands.RegisterClient;

public sealed class RegisterClientCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser)
    : IRequestHandler<RegisterClientCommand, Result<int>>
{
    public async Task<Result<int>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Clients.ExistsAsync(c => c.LibraryCardNumber == request.LibraryCardNumber))
            return Result<int>.Failure("This Library Card Number is already assigned to another client.");
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var person = request.ToEntity();
            unitOfWork.Repository<Person>().Add(person);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var userResult = await identityUser.CreateUserAsync(request.Email, request.Password, request.UserName,
                person.Id,
                request.PhoneNumber);

            if (userResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(userResult.Error);
            }
            
            var client = new Domain.Entities.Client
            {
                PersonId = person.Id,
                UserId = userResult.Data,
                LibraryCardNumber = request.LibraryCardNumber,
            };
            unitOfWork.Clients.Add(client);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var roleResult = await identityUser.AddUserToRoleAsync(request.UserName, Roles.Client);

            if (roleResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(roleResult.Error);
            }

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
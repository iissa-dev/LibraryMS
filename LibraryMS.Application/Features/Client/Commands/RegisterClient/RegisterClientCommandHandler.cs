namespace LibraryMS.Application.Features.Client.Commands.RegisterClient;

public sealed class RegisterClientCommandHandler(IIdentityUser identityUser, IAppDbContext context)
    : IRequestHandler<RegisterClientCommand, Result<int>>
{
    public async Task<Result<int>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
            if (await context.Clients.AnyAsync(c => c.LibraryCardNumber == request.LibraryCardNumber, cancellationToken))
                return Result<int>.Failure("This Library Card Number is already assigned to another client.");

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                CountryId = request.CountryId,
                DateOfBirth = request.BirthDate
            };
            context.People.Add(person);
            await context.SaveChangesAsync(cancellationToken);

            var userResult = await identityUser.CreateUserAsync(
                request.Email,
                request.Password,
                request.UserName,
                request.PhoneNumber,
                person.Id);

            if (userResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(userResult.Error);
            }

            var client = new Domain.Entities.Client
            {
                PersonId = person.Id,
                LibraryCardNumber = request.LibraryCardNumber,
            };
            context.Clients.Add(client);

            var roleResult = await identityUser.AddUserToRoleAsync(request.UserName, Roles.Client);

            if (roleResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(roleResult.Error);
            }

            await context.SaveChangesAsync(cancellationToken);
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
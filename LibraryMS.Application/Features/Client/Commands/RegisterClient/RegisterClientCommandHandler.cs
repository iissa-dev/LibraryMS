namespace LibraryMS.Application.Features.Client.Commands.RegisterClient;

public sealed class RegisterClientCommandHandler(IIdentityUser identityUser, IAppDbContext context, ICodeGeneratorService codeGenerator)
    : IRequestHandler<RegisterClientCommand, Result<int>>
{
    public async Task<Result<int>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
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


            var libraryCardNumber = codeGenerator.GenerateLibraryCardNumber();
            var client = new Domain.Entities.Client
            {
                PersonId = person.Id,
                LibraryCardNumber = libraryCardNumber,
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
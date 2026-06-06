namespace LibraryMS.Application.Features.Employee.Commands.CreateEmployeeAccount;

public sealed class CreateEmployeeCommandHandler(IAppDbContext context, IIdentityUser identityUser)
    : IRequestHandler<CreateEmployeeCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
            if (await context.Employees.AnyAsync(c => c.EmployeeCode == request.EmployeeCode, cancellationToken))
                return Result<int>.Failure("This Employee Number is already assigned to another employee.");

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


            var userResult = await identityUser.CreateUserAsync(request.Email, request.Password, request.UserName,
                request.PhoneNumber, person.Id, request.FirstName, request.LastName, request.CountryId, request.BirthDate);

            if (userResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(userResult.Error);
            }

            var employee = new Domain.Entities.Employee
            {
                PersonId = person.Id,
                EmployeeCode = request.EmployeeCode,
            };

            context.Employees.Add(employee);

            Result<int>? roleResult;
            if (request.RoleId == (short)Roles.Admin)
            {
                roleResult = await identityUser
                    .AddToRolesAsync(request.UserName, [Roles.Admin.ToString(), Roles.Employee.ToString()]);
            }
            else
            {
                roleResult = await identityUser.AddUserToRoleAsync(request.UserName, Roles.Employee);
            }


            if (roleResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<int>.Failure(roleResult.Error);
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result<int>.Success(employee.Id);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
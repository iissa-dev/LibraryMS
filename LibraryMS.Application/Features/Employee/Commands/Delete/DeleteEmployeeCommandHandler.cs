using LibraryMS.Application.Common.Extensions;

namespace LibraryMS.Application.Features.Employee.Commands.Delete;

public sealed class DeleteEmployeeCommandHandler(IAppDbContext context, IIdentityUser identityUser) : IRequestHandler<DeleteEmployeeCommand, Result>
{
    public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
            // Delete Employee
            var employee = await context.Employees
            .SingleOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);
            if (employee is null)
            {
                return Result.Failure("Employee not found.");
            }

            if (employee.IsDeleted)
            {
                return Result.Failure("Employee is already deleted.");
            }

            var userResult = await identityUser.ValidateUserPersonMatchAsync(request.UserId, employee.PersonId);
            if (userResult.IsFailure)
            {
                return Result.Failure(userResult.Error);
            }

            context.Employees.Remove(employee);

            // Delete User

            var deleteUserResult = await identityUser.DeleteUserAsync(request.UserId);
            if (deleteUserResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(deleteUserResult.Error);
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
        throw new NotImplementedException();
    }
}

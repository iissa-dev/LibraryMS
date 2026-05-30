namespace LibraryMS.Application.Features.Employee.Commands.Restore;

public sealed class RestoreEmployeeCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser) : IRequestHandler<RestoreEmployeeCommand, Result>
{
    public async Task<Result> Handle(RestoreEmployeeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            // UnDelete Employee
            var employee = await unitOfWork.Employees.GetDeletedEmployeeByIdAsync(request.UserId);
            if (employee is null) return Result.Failure("Employee not found");

            employee.UnDelete();
            
            // UnDeleteUser
            var userResult = await identityUser.RestoreUserAsync(request.UserId);
            if (userResult.IsFailure)
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

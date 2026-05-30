namespace LibraryMS.Application.Features.Employee.Commands.Delete;

public sealed class DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork, IIdentityUser identityUser) : IRequestHandler<DeleteEmployeeCommand, Result>
{
    public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            // Delete Employee
            var employee = await unitOfWork.Employees.GetEmployeeByUserIdAsync(request.UserId);
            if (employee is null)
            {
                return Result.Failure("Employee not found.");
            }
            unitOfWork.Employees.Delete(employee);

            // Delete User

            var userResult = await identityUser.DeleteUserAsync(request.UserId);
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
        throw new NotImplementedException();
    }
}

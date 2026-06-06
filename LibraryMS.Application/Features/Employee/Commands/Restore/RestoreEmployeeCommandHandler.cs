using LibraryMS.Application.Common.Extensions;

namespace LibraryMS.Application.Features.Employee.Commands.Restore;

public sealed class RestoreEmployeeCommandHandler(IAppDbContext context, IIdentityUser identityUser) : IRequestHandler<RestoreEmployeeCommand, Result>
{
    public async Task<Result> Handle(RestoreEmployeeCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
            // UnDelete Employee
            var employee = await context.Employees
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(e => e.Id == request.EmployeeId && e.IsDeleted, cancellationToken);

            if (employee is null) return Result.Failure("Employee not found");

            var userResult = await identityUser.CurrentUserByIdAsync(request.UserId);
            if (userResult.IsFailure)
            {
                return Result.Failure("User not found");
            }

            var user = userResult.Data;

            var securityResult = await identityUser.ValidateUserPersonMatchAsync(request.UserId, employee.PersonId);
            if (securityResult.IsFailure)
            {
                return Result.Failure(securityResult.Error);
            }

            employee.UnDelete();

            // UnDeleteUser
            var RestoreUserResult = await identityUser.RestoreUserAsync(request.UserId);
            if (RestoreUserResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(RestoreUserResult.Error);
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
    }
}

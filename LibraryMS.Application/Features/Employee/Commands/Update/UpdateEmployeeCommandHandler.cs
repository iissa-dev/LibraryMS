using LibraryMS.Application.DTOs.UserDto;

namespace LibraryMS.Application.Features.Employee.Commands.Update;

public class UpdateEmployeeCommandHandler(IAppDbContext context) : IRequestHandler<UpdateEmployeeCommand, Result>
{
    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Update this method if Employee properties are added in the future.
        var employee = await context.Employees.SingleOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);
        if (employee is null)
        {
            return Result.Failure("Employee not found");
        }

        employee.EmployeeCode = request.EmployeeCode;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;

    }
}
namespace LibraryMS.Application.Features.Employee.Commands.Update;

public sealed record UpdateEmployeeCommand(
    int EmployeeId,
    string EmployeeCode
) : IRequest<Result>;

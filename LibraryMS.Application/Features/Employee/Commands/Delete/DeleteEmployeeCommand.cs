namespace LibraryMS.Application.Features.Employee.Commands.Delete;

public sealed record DeleteEmployeeCommand(int UserId, int EmployeeId) : IRequest<Result>;

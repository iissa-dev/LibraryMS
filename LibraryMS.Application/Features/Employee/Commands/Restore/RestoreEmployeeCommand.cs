namespace LibraryMS.Application.Features.Employee.Commands.Restore;

public sealed record RestoreEmployeeCommand(int UserId, int EmployeeId) : IRequest<Result>;

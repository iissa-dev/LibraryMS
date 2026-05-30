namespace LibraryMS.Application.Features.Employee.Commands.Restore;

public sealed record RestoreEmployeeCommand(int UserId) : IRequest<Result>;

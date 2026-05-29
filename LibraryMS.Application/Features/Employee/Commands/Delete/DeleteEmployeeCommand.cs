using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Commands.Delete;

public sealed record DeleteEmployeeCommand(int UserId) : IRequest<Result>;

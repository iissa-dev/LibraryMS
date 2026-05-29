using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Commands.Update;

public sealed record UpdateEmployeeCommand(
    int UserId,
    string FirstName,
    string LastName,
    string Address,
    string EmployeeCode,
    string PhoneNumber,
    string Email,
    string UserName,
    string? ImageUrl,
    DateOnly DateOfBirth,
    int CountryId
) : IRequest<Result>;

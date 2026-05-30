namespace LibraryMS.Application.Features.Employee.Commands.CreateEmployeeAccount;

public sealed record CreateEmployeeCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    string Address,
    string UserName,
    int CountryId,
    DateOnly BirthDate,
    string? ImageUrl,
    string EmployeeCode,
    short RoleId
) : IRequest<Result<int>>;
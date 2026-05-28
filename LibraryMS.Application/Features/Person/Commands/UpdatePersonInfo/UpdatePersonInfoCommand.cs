using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Person.Commands.UpdatePersonInfo;

public sealed record UpdatePersonInfoCommand(
    int UserId,
    string FirstName,
    string LastName,
    string Address,
    DateOnly BirthDate,
    string? ImageUrl
) : IRequest<Result>;
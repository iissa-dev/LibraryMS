using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Client.Commands.RegisterClient;

public sealed record RegisterClientCommand(
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
    string LibraryCardNumber
) : IRequest<Result<int>>;
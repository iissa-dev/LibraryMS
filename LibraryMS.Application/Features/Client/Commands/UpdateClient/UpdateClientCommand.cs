namespace LibraryMS.Application.Features.Client.Commands.UpdateClient;

public sealed record UpdateClientCommand(
    int UserId,
    string FirstName,
    string LastName,
    string Address,
    string LibraryCardNumber,
    string PhoneNumber,
    string Email,
    string UserName,
    string? ImageUrl,
    DateOnly DateOfBirth,
    int CountryId
) : IRequest<Result>;
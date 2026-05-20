using LibraryMS.Application.Features.Client.Commands.RegisterClient;

namespace LibraryMS.Application.DTOs.ClientDto;

public class ClientRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public int CountryId { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? ImageUrl { get; set; }
    public string LibraryCardNumber { get; set; }

    public RegisterClientCommand ToCommand()
    {
        return new RegisterClientCommand(
            Email,
            Password,
            FirstName,
            LastName,
            PhoneNumber,
            Address,
            UserName,
            CountryId,
            BirthDate,
            ImageUrl,
            LibraryCardNumber
        );
    }
}
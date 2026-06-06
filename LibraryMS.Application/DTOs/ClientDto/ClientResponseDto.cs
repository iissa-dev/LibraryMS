namespace LibraryMS.Application.DTOs.ClientDto;

public class ClientResponseDto
{
    public int ClientId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LibraryCardNumber { get; set; } = string.Empty;
    public DateTime CreatedOn { get; init; }
    public string? Country { get; set; } = string.Empty;
}
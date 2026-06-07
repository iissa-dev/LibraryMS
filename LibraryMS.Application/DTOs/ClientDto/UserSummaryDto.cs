namespace LibraryMS.Application.DTOs.ClientDto;

public class ClientSummaryDto
{
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string LibraryCardNumber { get; set; } = string.Empty;
}
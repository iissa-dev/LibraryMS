namespace LibraryMS.Application.DTOs.UserDto;

public class UserSummaryDto
{
    public int UserId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string LibraryCardNumber { get; set; } = string.Empty;
}
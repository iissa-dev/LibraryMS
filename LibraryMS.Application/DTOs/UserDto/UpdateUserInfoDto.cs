namespace LibraryMS.Application.DTOs.UserDto;

public class UpdateUserInfoDto
{

    public int UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}
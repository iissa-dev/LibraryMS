namespace LibraryMS.Application.DTOs.UserDto;
public class UpdateUserInfoDto
{

    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int CountryId { get; set; } 
}
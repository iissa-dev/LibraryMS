namespace LibraryMS.Application.DTOs.AuthDto;

public class CurrentUserDto
{
    public int UserId { get; set; }
    public int PersonId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? PhoneNumber {get; set;}
    public string? ImageUrl { get; set; }

    public DateOnly DateOfBirth { get; set; }

}
namespace LibraryMS.Application.DTOs.ClientDto;

public class ClientResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string LibraryCardNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime CreatedOn { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
}
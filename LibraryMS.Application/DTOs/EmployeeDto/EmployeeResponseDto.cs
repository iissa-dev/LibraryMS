namespace LibraryMS.Application.DTOs.EmployeeDto;
public class EmployeeResponseDto
{
     public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Address { get; set; }= string.Empty;
    public string EmployeeCode { get; set; }= string.Empty;
    public string PhoneNumber { get; set; }= string.Empty;
    public string Email { get; set; }= string.Empty;
    public DateTime CreatedOn { get; init; }
    public int UserId { get; set; }
    public string Username { get; set; }= string.Empty;
}
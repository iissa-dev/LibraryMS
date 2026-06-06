namespace LibraryMS.Application.DTOs.EmployeeDto;

public class EmployeeResponseDto
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public DateTime CreatedOn { get; init; }
    public string Country { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}
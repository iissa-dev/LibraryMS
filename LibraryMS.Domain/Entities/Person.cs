namespace LibraryMS.Domain.Entities;

public class Person : BaseEntity
{
    public string FirstName { get; set; }= string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int CountryId { get; set; }
    public string? ImageUrl { get; set; }
    

    public Country Country { get; set; } = null!;
}
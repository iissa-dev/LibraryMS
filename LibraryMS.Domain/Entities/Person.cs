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

    public void Update(string FirstName, string LastName, string Address, string? ImageUrl, DateOnly DateOfBirth)
    {
        this.FirstName =  FirstName;
        this.LastName = LastName;
        this.Address = Address;
        this.ImageUrl = ImageUrl;
        this.DateOfBirth = DateOfBirth;
    }
}
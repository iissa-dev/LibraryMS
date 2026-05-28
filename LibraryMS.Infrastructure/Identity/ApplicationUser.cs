using LibraryMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryMS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
     public string FirstName { get; set; }= string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int CountryId { get; set; }
    public string? ImageUrl { get; set; }

    public Country? Country {get; set;}
    
}
namespace LibraryMS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>, ISoftDeleteable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int CountryId { get; set; }
    public string? ImageUrl { get; set; }

    public Country? Country { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOn = DateTime.UtcNow;
    }

    public void UnDelete()
    {
        IsDeleted = false;
        DeletedOn = null;
    }
}
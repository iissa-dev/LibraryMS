namespace LibraryMS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>, ISoftDeleteable
{
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

    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
}
using Microsoft.AspNetCore.Identity;

namespace LibraryMS.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public int? PersonId { get; set; }
}
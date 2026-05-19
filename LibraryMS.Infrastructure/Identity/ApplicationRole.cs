using Microsoft.AspNetCore.Identity;

namespace LibraryMS.Infrastructure.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
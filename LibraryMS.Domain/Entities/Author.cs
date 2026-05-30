using LibraryMS.Domain.Common;

namespace LibraryMS.Domain.Entities;

public class Author : BaseEntity, ISoftDeleteable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;

    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
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

    public void Update(string firstName, string lastName, string biography)
    {
        FirstName = firstName;
        LastName = lastName;
        Biography = biography;
    }
}
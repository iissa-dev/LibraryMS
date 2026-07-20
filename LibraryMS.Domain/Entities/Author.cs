namespace LibraryMS.Domain.Entities;

public class Author : SoftDeleteableEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;

    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    public void Update(string firstName, string lastName, string biography)
    {
        FirstName = firstName;
        LastName = lastName;
        Biography = biography;
    }
}
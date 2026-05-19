namespace LibraryMS.Domain.Entities;

public class Author : BaseEntity
{
    public int PersonId { get; set; }                        
    public string Biography { get; set; } =  string.Empty;

    public Person Person { get; set; } = null!;
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}
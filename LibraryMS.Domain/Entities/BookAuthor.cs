namespace LibraryMS.Domain.Entities;

public class BookAuthor : BaseEntity
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }

    public Author Author { get; set; } = null!;
    public Book Book { get; set; } = null!;
}
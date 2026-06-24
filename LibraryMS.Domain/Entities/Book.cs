using LibraryMS.Domain.Exceptions;

namespace LibraryMS.Domain.Entities;

public class Book : BaseEntity, ISoftDeleteable
{
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public Genre Genre { get; set; } = Genre.Other;
    public string? AdditionalDetails { get; set; }
    public string? BookImageUrl { get; set; }


    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
    public void Delete()
    {
        IsDeleted = true;
        DeletedOn = DateTime.UtcNow;

        foreach (var copy in Copies)
        {
            copy.CopyStatus = CopyStatus.Archived;
            copy.Delete();
        }
    }

    public void UnDelete()
    {
        IsDeleted = false;
        DeletedOn = null;
        foreach (var copy in Copies)
        {
            copy.CopyStatus = CopyStatus.Available;
            copy.UnDelete();
        }
    }

    public void AddCopy(string serialNumber)
    {
        if (Copies.Any(c => c.SerialNumber == serialNumber))
            throw new DomainException("This serial number already exists for this book.");

        var copy = new BookCopy
        {
            CopyStatus = CopyStatus.Available,
            SerialNumber = serialNumber
        };
        Copies.Add(copy);
    }

    public void AddBookAuthors(List<int> authorIds)
    {
        foreach (var authorId in authorIds)
        {
            BookAuthors.Add(new BookAuthor
            {
                AuthorId = authorId
            });
        }
    }

    public void UpdateBookAuthors(List<int> authorIds)
    {
        var authorsToRemove = BookAuthors
        .Where(ba => !authorIds.Contains(ba.AuthorId))
        .ToList();

        foreach (var bookAuthor in authorsToRemove)
        {
            BookAuthors.Remove(bookAuthor);
        }

        var existingAuthorIds = BookAuthors.Select(ba => ba.AuthorId).ToHashSet();

        foreach (var authorId in authorIds)
        {
            if (!existingAuthorIds.Contains(authorId))
            {
                BookAuthors.Add(new BookAuthor
                {
                    AuthorId = authorId
                });
            }
        }
    }
    public void UpdateBookDetails(string title, string isbn, DateTime publishDate, Genre genre, string additionalDetails, string bookImageUrl)
    {
        Title = title;
        ISBN = isbn;
        PublishDate = publishDate;
        Genre = genre;
        AdditionalDetails = additionalDetails;
        BookImageUrl = bookImageUrl;
    }
}
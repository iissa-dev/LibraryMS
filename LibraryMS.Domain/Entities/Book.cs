
using LibraryMS.Domain.Common;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Domain.Entities;
public class Book : BaseEntity, ISoftDeleteable
{
    public string Title { get; set; } =  string.Empty;
    public string  ISBN { get; set; }  = string.Empty;
    public DateTime PublishDate { get; set; }
    public Genre Genre { get; set; } = Genre.Other;
    public string? AdditionalDetails { get; set; }
    public string? BookImageUrl { get; set; }
    
    
    public ICollection<BookAuthor> Authors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
    public void Delete()
    {
        IsDeleted = true;
        DeletedOn = DateTime.Now;
    }

    public void UnDelete()
    {
        IsDeleted = false;
        DeletedOn = null;
    }
}
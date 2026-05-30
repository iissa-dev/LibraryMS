using LibraryMS.Domain.Common;

namespace LibraryMS.Domain.Entities;

public class BookCopy : BaseEntity, ISoftDeleteable
{
    public int BookId { get; set; }
    public bool IsAvailable { get; set; }

    public string SerialNumber { get; set; } = string.Empty;

    public Book Book { get; set; } = null!;
    public ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();

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
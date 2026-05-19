using LibraryMS.Domain.Common;

namespace LibraryMS.Domain.Entities;

public class BookCopy : BaseEntity
{
    public int BookId { get; set; }
    public bool IsAvailable { get; set; }

    public Book Book { get; set; } = null!;
    public ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();
}
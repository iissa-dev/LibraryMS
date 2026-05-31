using LibraryMS.Domain.Exceptions;

namespace LibraryMS.Domain.Entities;

public class BookCopy : BaseEntity, ISoftDeleteable
{
    public int BookId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public CopyStatus CopyStatus { get; set; } = CopyStatus.Available;
    public Book Book { get; set; } = null!;
    public ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();

    public bool IsAvailable => CopyStatus == CopyStatus.Available;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
    public void Delete()
    {
        if(IsDeleted) return;
        IsDeleted = true;
        CopyStatus = CopyStatus.Archived;
        DeletedOn = DateTime.UtcNow;
    }

    public void UnDelete()
    {
        if (!IsDeleted)
        {
            throw new DomainException("This book copy is not deleted or archived to be restored.");
        }

        CopyStatus = CopyStatus.Available;
        IsDeleted = false;
        DeletedOn = null;
    }

    public void UpdateStatus(CopyStatus newStatus)
    {
        if (CopyStatus == CopyStatus.Borrowed && newStatus != CopyStatus.Borrowed)
        {
            throw new DomainException("Cannot manually change status of a currently borrowed book. It must be returned first.");
        }

        CopyStatus = newStatus;
    }
}
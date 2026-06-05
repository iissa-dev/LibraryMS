namespace LibraryMS.Domain.Entities;

public class BorrowingRecord : BaseEntity
{
    public int ClientId { get; set; }
    public int CopyId { get; set; }
    public DateTime BorrowingDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }

    public bool IsLate => !ActualReturnDate.HasValue && DateTime.UtcNow > DueDate;

    public int LateDate
    {
        get
        {
            if (!IsLate) return 0;
            var timeSpan = DateTime.UtcNow - DueDate;
            return (int)Math.Ceiling(timeSpan.TotalDays);
        }
    }
    public Client Client { get; set; } = null!;
    public BookCopy BookCopy { get; set; } = null!;
    public Fine? Fine { get; set; }

    public void MarkAsReturned()
    {
        ActualReturnDate = DateTime.UtcNow;
    }

    public void AddFine(Fine fine)
    {
        if(!IsLate) return;
        Fine = fine;
    }
}
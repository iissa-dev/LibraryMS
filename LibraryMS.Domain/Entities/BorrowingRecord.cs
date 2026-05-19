namespace LibraryMS.Domain.Entities;

public class BorrowingRecord : BaseEntity
{
    public int ClientId { get; set; }
    public int CopyId { get; set; }
    public DateTime BorrowingDate { get; set; } =  DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }

    public Client Client { get; set; } = null!;
    public BookCopy BookCopy { get; set; } = null!;
    public Fine?  Fine { get; set; }
}
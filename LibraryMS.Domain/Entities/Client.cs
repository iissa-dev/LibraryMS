namespace LibraryMS.Domain.Entities;

public class Client : BaseEntity
{
    public string LibraryCardNumber { get; set; } =  string.Empty;
    public int UserId { get; set; }
    public ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Fine> Fines { get; set; } = new List<Fine>();
}
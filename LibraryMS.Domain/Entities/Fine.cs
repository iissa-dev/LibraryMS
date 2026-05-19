using LibraryMS.Domain.Enums;

namespace LibraryMS.Domain.Entities;

public class Fine : BaseEntity
{
    public int ClientId { get; set; }
    public int BorrowingRecordId { get; set; }
    public int NumberOfLateDays { get; set; }
    public decimal FineAmount { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
    public string Reason { get; set; } = string.Empty;

    public Client Client { get; set; } = null!;
    public BorrowingRecord BorrowingRecord { get; set; } = null!;
}
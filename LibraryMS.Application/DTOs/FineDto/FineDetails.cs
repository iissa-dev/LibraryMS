using LibraryMS.Application.DTOs.UserDto;

namespace LibraryMS.Application.DTOs.FineDto;

public class FineDetails
{
    public int FineId { get; set; }
    public DateTime BorrowingDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public decimal FineAmount { get; set; }

    public UserSummaryDto Borrower { get; set; } = null!;
}
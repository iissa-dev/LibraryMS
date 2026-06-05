using LibraryMS.Application.DTOs.BookDtos;
using LibraryMS.Application.DTOs.UserDto;

namespace LibraryMS.Application.DTOs.BorrowDto;

public class BorrowDetails
{
    public int BorrowId { get; set; }
    public BookSummaryDto Book { get; set; } = null!;
    public UserSummaryDto Borrower { get; set; } = null!;

    public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal? FineAmount { get; set; }

}
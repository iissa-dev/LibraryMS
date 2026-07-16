using LibraryMS.Application.DTOs.BookDtos;
using LibraryMS.Application.DTOs.ClientDto;

namespace LibraryMS.Application.DTOs.BorrowDto;

public class BorrowDetails
{
    public int BorrowId { get; set; }
    public BookSummaryDto Book { get; set; } = null!;
    public ClientSummaryDto Borrower { get; set; } = null!;

    public int CopyId { get; set; }
    public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal? FineAmount { get; set; }

}
namespace LibraryMS.Application.DTOs.BookDtos;

public class BookSummaryDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public IEnumerable<string> Author { get; set; } = [];
}
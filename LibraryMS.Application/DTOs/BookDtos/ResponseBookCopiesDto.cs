namespace LibraryMS.Application.DTOs.BookDtos;

public class ResponseBookCopiesDto
{
    public int BookCopyId { get; set; }
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;

    public string SerialNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
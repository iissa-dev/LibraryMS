using LibraryMS.Application.DTOs.AuthorDto;

namespace LibraryMS.Application.DTOs.BookDtos;

public class ResponseBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public Genre Genre { get; set; }
    public string? AdditionalDetails { get; set; }
    public string? BookImageUrl { get; set; }

    public List<AuthorResponseDto> Authors { get; set; } = [];
}
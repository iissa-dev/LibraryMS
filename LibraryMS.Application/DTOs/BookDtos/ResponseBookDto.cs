using System.Linq.Expressions;
using LibraryMS.Application.DTOs.AuthorDto;

namespace LibraryMS.Application.DTOs.BookDtos;

public class ResponseBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string? AdditionalDetails { get; set; }
    public string? BookImageUrl { get; set; }

    public bool IsDeleted { get; set; }

    public List<AuthorResponseDto> Authors { get; set; } = [];

    public static Expression<Func<Book, ResponseBookDto>> Projection =>
        book => new ResponseBookDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.ISBN,
            PublishDate = book.PublishDate,
            Genre = book.Genre.ToString(),
            AdditionalDetails = book.AdditionalDetails,
            BookImageUrl = book.BookImageUrl,
            IsDeleted = book.IsDeleted,
            Authors = book.BookAuthors.Select(a => new AuthorResponseDto
            {
                Id = a.AuthorId,
                FullName = $"{a.Author.FirstName} {a.Author.LastName}"
            }).ToList()
        };
}
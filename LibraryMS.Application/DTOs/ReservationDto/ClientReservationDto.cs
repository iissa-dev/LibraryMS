namespace LibraryMS.Application.DTOs.ReservationDto;

public class ClientReservationDto
{
    public int ReservationId {get; init;}

    public int BookId { get; init; }
    public string BookTitle { get; init; } = string.Empty;
    public IEnumerable<string> AuthorName { get; init; } = [];

    public DateTime ReservationDate { get; init; }
    public string StatusName { get; init; } = string.Empty;

    public int? BookCopyId { get; init; }
    public int QueuePosition { get; init; }
}
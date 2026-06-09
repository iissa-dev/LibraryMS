namespace LibraryMS.Application.DTOs.ReservationDto;

public class ReservationNotificationDto
{
    public string Message { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public int ReservationId { get; init; }
}
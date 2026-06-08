using LibraryMS.Domain.Exceptions;

namespace LibraryMS.Domain.Entities;

public class Reservation : BaseEntity
{
    public int ClientId { get; set; }
    public int BookId { get; set; }
    public int? BookCopyId { get; set; }
    public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
    public ReservationsStatus ReservationsStatus { get; set; } = ReservationsStatus.Waiting;

    public Client Client { get; set; } = null!;
    public Book Book { get; set; } = null!;

    public BookCopy BookCopy { get; set; } = null!;

    public void CancelReservation()
    {
        if (ReservationsStatus == ReservationsStatus.Cancelled)
            throw new DomainException("Reserve is already cancelled");

        if (ReservationsStatus == ReservationsStatus.Completed)
            throw new DomainException("Connot cancel a completed reservation");

        ReservationsStatus = ReservationsStatus.Cancelled;
    }
}
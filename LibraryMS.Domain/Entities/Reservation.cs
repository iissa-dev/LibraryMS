using LibraryMS.Domain.Exceptions;

namespace LibraryMS.Domain.Entities;

public class Reservation : BaseEntity
{
    public int ClientId { get; set; }
    public int BookId { get; set; }
    public int? BookCopyId { get; set; }
    public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
    public ReservationsStatus ReservationsStatus { get; set; } = ReservationsStatus.Waiting;
    public bool ReadyToBorrow => ReservationsStatus == ReservationsStatus.ReadyForPickup || ReservationsStatus == ReservationsStatus.Notified;

    public Client Client { get; set; } = null!;
    public Book Book { get; set; } = null!;

    public BookCopy? BookCopy { get; set; }

    public void CancelReservation()
    {
        if (ReservationsStatus == ReservationsStatus.Cancelled)
            throw new DomainException("Reserve is already cancelled");

        if (ReservationsStatus == ReservationsStatus.Completed)
            throw new DomainException("Connot cancel a completed reservation");

        ReservationsStatus = ReservationsStatus.Cancelled;
    }

    public void Fulfill(BookCopy copy)
    {
        if (!ReadyToBorrow)
        {
            throw new DomainException("Reservation is not ready to be fulfilled.");
        }

        ReservationsStatus = ReservationsStatus.Completed;
        copy.UpdateStatus(CopyStatus.Borrowed);
    }
}
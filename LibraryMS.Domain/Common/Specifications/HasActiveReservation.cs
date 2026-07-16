using LibraryMS.Domain.Entities;
namespace LibraryMS.Domain.Common.Specifications;

public sealed class HasActiveReservation : BaseSpecification<Reservation>
{
    public HasActiveReservation(int CopyId)
    {
        Query = r => r.BookCopyId == CopyId &&
        (r.ReservationsStatus == ReservationsStatus.ReadyForPickup || r.ReservationsStatus == ReservationsStatus.Notified);
    }
}
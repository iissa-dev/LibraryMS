using LibraryMS.Domain.Entities;
namespace LibraryMS.Domain.Common.Specifications;

public sealed class HasWaitingQueueSpecification : BaseSpecification<Reservation>
{
    public HasWaitingQueueSpecification(int BookId)
    {
        Query = r => r.BookId == BookId && r.ReservationsStatus == ReservationsStatus.Waiting;
    }
}

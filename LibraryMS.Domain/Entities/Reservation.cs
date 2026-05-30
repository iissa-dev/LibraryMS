namespace LibraryMS.Domain.Entities;

public class Reservation : BaseEntity
{
    public int ClientId { get; set; }
    public int BookId { get; set; }
    public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
    public ReservationsStatus ReservationsStatus { get; set; } = ReservationsStatus.Waiting;

    public Client Client { get; set; } = null!;
    public Book Book { get; set; } = null!;
}
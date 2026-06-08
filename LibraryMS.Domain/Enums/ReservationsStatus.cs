namespace LibraryMS.Domain.Enums;

public enum ReservationsStatus
{
    Waiting = 1,
    ReadyForPickup,
    Notified,
    Completed,
    Cancelled,
}
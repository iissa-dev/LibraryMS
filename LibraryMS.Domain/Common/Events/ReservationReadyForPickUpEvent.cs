using MediatR;

namespace LibraryMS.Domain.Common.Events;

public record ReservationReadyForPickUpEvent(int ReservationId, int ClientId, string BookTitle) : INotification;